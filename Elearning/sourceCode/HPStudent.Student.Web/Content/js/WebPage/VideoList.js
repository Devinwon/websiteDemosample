$(function () {
    //页面打开时直接显示所有项
    ShowProjectList(0, 1);


});

function ShowProjectList(CID, INDEX) {

    //3. AJAX动态调用后台接口获得所有短训推荐项目
    var myGridview = $('#gridview').GridView({
        ajaxUrl: '/Projects/GetProjectListByShort',
        ajaxData: { CID: CID },
        pageSize: 12,
        pageIndex: INDEX,
        pageDiv: 'pageDiv',
        titleLength: 20,
        template: '<a class="gallery-item" href="/WebPage/VideoDetail/?id=yoyotui_pid" title="yoyotui_title" data-gallery><div class="image"><img src="yoyotui_img" alt="yoyotui_title" /></div><div class="meta"><strong> yoyotui_title </strong></div></a>',
    });
    onresize();
}