<template>
  <Card>
      <Row :gutter="10" class="padding-10">
          <Col span="4"><Input v-model="serach.id" placeholder="序号" /></Col>
          <Col span="4"><Input v-model="serach.title" placeholder="标题" /></Col>
          <Col span="4"><Input v-model="serach.userName" placeholder="创建者" /></Col>
          <Col span="3"><DatePicker type="date" v-model="serach.createdDate" format="yyyy-MM-dd" placeholder="创建时间" ></DatePicker></Col>
        <Button class="search-btn" type="primary" @click="getArticles" ><Icon type="search"/>&nbsp;&nbsp;搜索</Button> &nbsp;
        <Button class="search-btn" type="primary" @click="editArticle()" ><Icon type="search"/>&nbsp;&nbsp;创建</Button>
    </Row>
      <Table highlight-row :columns="[
                    {
                        title: '序号',
                        key: 'id',
                        width: 300,
                    },
                    {
                        title: '标题',
                        key: 'title'
                    },
                    {
                        title: '栏目',
                        key: 'channelName',
                        width: 150
                    },
                    {
                        title: '用户',
                        key: 'userName',
                        width: 150
                        
                    },
                    {
                        title: '创建时间',
                        key: 'created',
                        render: (h,params)=>{
                            created = new Date(params.row.created);
                            return h('Time',{
                                props: {
                                        time: params.row.created,
                                        type: created.setDate(created.getDate() + 1) < new Date() ? 'datetime': 'relative'
                                    }
                                }
                            )
                        }
                    },
                    {
                        title: '操作',
                        key: 'action',
                        width: 150,
                        align: 'center',
                        render: (h, params) => {
                            return h('div', [
                                h('Button', {
                                    props: {
                                        type: 'primary',
                                        size: 'small'
                                    },
                                    style: {
                                        marginRight: '10px'
                                    },
                                    on: {
                                        click: () => {
                                            this.editArticle(params.row.id)
                                        }
                                    }
                                }, '编辑'),
                                h('Button', {
                                    props: {
                                        type: 'error',
                                        size: 'small'
                                    },
                                    on: {
                                        click: () => {
                                            this.delArticle(params.row.id)
                                        }
                                    }
                                }, '删除')
                            ]);
                        }
                    }
                ]" :data="articles"></Table>
                <Row class="padding-10 text-right"><Page :total="articles.total" show-sizer @on-change="pageChange" @on-page-size-change="pageSizeChange" /></Row>
        <Drawer
            :title="articleForm.title"
            v-model="articleForm.showDrawer"
            width="720"
        >
        <Form :model="article" ref="articleForm" :rules="articleFormRules" :label-width="80">
            <FormItem label="标题" prop="title">
                <Input v-model="article.title" placeholder="请输入文章标题"></Input>
            </FormItem>
            <FormItem label="副标题">
                <Input v-model="article.subTitle" placeholder="请输入补充标题"></Input>
            </FormItem>
            <Row>
                <Col span="20">
                <FormItem label="栏目" prop="channelId">
                    <Cascader :data="channels" :load-data="getChildrenChannels" change-on-select  v-model="article.channelId" placeholder="请选择文章栏目"></Cascader>
                </FormItem>
                </Col>
                &nbsp;
                <Poptip width="400"
                    title="在当前栏目下添加">
                    <Button>添加</Button>
                    <div slot="content">
                        <Input v-model="channel.title" placeholder="请输入栏目名称"></Input>
                        <br><br>
                        <Button type="primary" class="pull-right" @click="addChannel" >保存</Button>
                    </div>
                </Poptip>
                 
            </Row>
            <FormItem label="来源">
                <Input v-model="article.author" placeholder="原创文章"></Input>
            </FormItem>
            <FormItem label="简介">
                <Input type="textarea" v-model="article.summary" placeholder="内容简介"></Input>
            </FormItem>
            <FormItem label="Markdown">
                <Tooltip theme="light" :disabled="articleForm.disabledEditorTooltip" content="已输入文章内容，无法切换编辑器。" placement="right" >
                    <i-switch size="large" :disabled="articleForm.disabledEditorSwitch" @on-change="editorSwitchChange">
                            <span slot="open">使用</span>
                            <span slot="close">关闭</span>
                        </i-switch>
                </Tooltip>
                
            </FormItem>
            <FormItem label="内容">
                <div v-if="article.editor === 1">
                    <mavon-editor ref=mavon :subfield="false" @change="mavonEditorChange" @imgAdd="mavonImgAdd" @imgDel="mavonImgDel" v-model="article.mdContent" ></mavon-editor>
                </div>
                <div v-if="!article.editor">
                    <quill-editor :options="quillOptions" @change="quillEditorChange($event.html)" v-model="article.content" >
                </quill-editor>
                </div>
            </FormItem>
            <FormItem>
                <Button type="primary" class="pull-right" @click="submitArticle">提交</Button>
            </FormItem>
        </Form>
        </Drawer>  
    </Card>
</template>

<script lang="ts" src="./article.ts"></script>