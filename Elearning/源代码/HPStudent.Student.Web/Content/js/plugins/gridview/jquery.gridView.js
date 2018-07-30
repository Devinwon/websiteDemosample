/*! GridView 1.0.0
* @2015 yoyotui.com/license
*/

(function (window, document, undefined) {
    (function (factory) {
        "use strict";

        if (typeof define === 'function' && define.amd) {
            // Define as an AMD module if possible
            define('GridView', ['jquery'], factory);
        }
        else if (typeof exports === 'object') {
            // Node/CommonJS
            module.exports = factory(require('jquery'));
        }
        else if (jQuery && !jQuery.fn.GridView) {
            // Define using browser globals otherwise
            // Prevent multiple instantiations if the script is loaded twice
            factory(jQuery);
        }
    }

    (/** @lends <global> */function ($) {
        "use strict";
        var gridview;

        /*
        * 私有变量
        */
        var _HtmlGridview = "";     //Gridview主体HTML
        var _HtmlPages = "";        //分页主体HTML
        //var _PageNum = 0;       //
        var _TotalRecord = 0;       //
        var _PageButton = new Array();      //


        $.fn.GridView = function (callGridview) {
            var templateHtml = '';
            templateHtml += '<a class="gallery-item" yoyotui_disable href="/yoyotui_url" title="yoyotui_title" data-gallery>';
            templateHtml += '<div class="image" ><img src="yoyotui_img" alt="yoyotui_title" /></div>';
            templateHtml += '<div class="meta"><strong style="text-align:center"> yoyotui_title </strong></div>';
            templateHtml += '<div class="image_text" >';
            templateHtml += '   <div class="image_text_alignCenter">';
            templateHtml += '       <div class="image_text_body">';
            templateHtml += '           <div class="image_text_title">';
            templateHtml += '                <span class="yoyotui_lock"></span>';
            templateHtml += '           </div>';
            templateHtml += '       </div>';
            templateHtml += '   </div>';
            templateHtml += '</div>';
            templateHtml += '</a>';
            //参数设置
            gridview = $.extend({
                /*
                 * 异步数据URL
                 */
                ajaxUrl: null,
                ajaxType: 'POST',
                ajaxData: null,
                dataType: 'json',

                /*
                 * 每页显示的项数
                 */
                pageClassname: ".gridview_page",
                pageSize: 10,
                PageButton: "paginate_button",
                PageButtonActive: "current",
                PageButtonDisabled: "disabled",

                /*
                 *
                 */
                titleLength: 0,
                /*
                 * 当前页数
                 */
                pageIndex: 1,

                /*
                 * 第几次调用，每调用一次，加1
                 */
                draw: 0,

                /*
                 * 获取总记录数，便于计算分页
                 */
                recordsTotal: 0,

                /*
                 * 分页的元素ID
                 */
                pageDiv: '#pageDiv',

                template: templateHtml,
                empty_template: '<div class="gridview_empty">没有找到符合条件的数据</div>',

            }, callGridview || {});

            //console.log("传入的参数：" + gridview.ajaxUrl);
            //初始化GridView
            _fnInitGridview(this);

            //创建分页

            //异步调用后台数据
            gridview.recordsTotal = 35;





            //console.log("传入的参数：" + gridview.photo);
            //console.log("pageDiv:" + gridview.pageDiv);
            //this.removeClass('btn-primary');
            //this.addClass("btn-success");
            return this;
        };

        //初始化Gridview
        function _fnInitGridview($obj) {

            //01.创建Gridview
            _fnCreateGridview($obj);
        }

        //创建Gridview函数
        function _fnCreateGridview($obj) {
            //设置加载提示
            $obj.prepend('<div>数据加载中...</div>');

            //ajax获取相关数据
            var returnDatas = _fnAjaxLoad($obj);



            //填充Div
            //for (var i = 0 ; i < gridview.pageSize ; i++) {
            //    $obj.append(gridview.template);
            //}
        }

        //创建分页
        //function _fnCreatePage($obj){

        //    var _retHtml = "";
        //    for(var i=0,ien = 7; i<ien ; i++)
        //    {
        //        if (i == 0) {
        //            _retHtml
        //        }
        //    }

        //    //gridview.pageIndex;

        //}
        //创建分页按钮
        function _fnCreatePageButton($obj) {
            var buttons = _fnNumbers();
            var button, btnClass, btnDisplay, node, pageNumber;

            //清空分页
            $(gridview.pageClassname).empty();

            for (var i = 0, ien = buttons.length ; i < ien ; i++) {
                button = buttons[i];
                switch (button) {
                    case 'previous':
                        btnDisplay = "上一页";
                        pageNumber = parseInt(gridview.pageIndex) - 1;
                        btnClass = _fnButtonClass(pageNumber);
                        break;
                    case 'next':
                        btnDisplay = "下一页";
                        pageNumber = parseInt(gridview.pageIndex) + 1;
                        btnClass = _fnButtonClass(pageNumber);
                        break;
                    default:
                        btnDisplay = button;
                        pageNumber = parseInt(button);
                        btnClass = _fnButtonClass(pageNumber)
                        //btnClass = (gridview.pageIndex === button) ? "active" : "";
                        break;
                }

                //创建HTML元素节点
                node = $('<li>', {
                    'class': btnClass
                }).append(
                        $('<a>', {
                            'data-dt-idx': pageNumber,
                            'href': 'javascript:void()'
                        }).html(btnDisplay)
                        );
                //给按钮绑定动作
                _fnBindAction(node, $obj, pageNumber, btnClass);
                //将节点附加到页面上去
                $(gridview.pageClassname).append(node);

            }

            ////_PageButton
            //var _pageNum = Math.ceil(_TotalRecord / gridview.pageSize);
            //var node;

            //for (var i = 0,  ien = 7; i < ien ; i++) {
            //    var node = $('<li></li>');
            //    if (i == 0) {
            //        if (pageview.pageIndex == 1)
            //        node = $('<li>', {
            //            'class': 'disabled',                       
            //        })
            //        .append('<a data-dt-idx="' + 'previous' + '">»</a>');                    
            //    } else if (i == 7) {
            //        node = $('<li>', {
            //            'class': 'disabled',
            //        })
            //        .append('<a data-dt-idx="' + 'last' + '">»</a>');
            //    }
            //}
        }

        //给按钮绑定动作
        function _fnBindAction(n, $obj, pageNumber, className) {
            $(n).find('a').bind('click', function (e) {
                if (className == "disabled" || className == "active") {
                    //如果按钮不可用或为当前页在点击时不做任何处理
                    return;
                } else {
                    //改变页数
                    _fnPageChange($obj, pageNumber)
                }


            });
        }

        function _fnPageChange($obj, pageNumber) {

            gridview.pageIndex = pageNumber;


            //重建Gridview
            _fnCreateGridview($obj);
        }

        //根据参数计算按钮是否可用
        function _fnButtonClass(pagenumber) {
            var maxpage = Math.ceil(_TotalRecord / gridview.pageSize);
            if (pagenumber == 0 || pagenumber > maxpage) {
                //当前页为第一页或最后一页
                return "disabled";
            } else if (pagenumber == gridview.pageIndex) {
                return "active";
            } else {
                return " ";
            }
            return " ";

        }

        //根据参数计算分页按钮中显示的内容
        function _fnNumbers() {
            var numbers = [];
            var maxpage, pagenum;

            for (var i = 0, ien = 7; i < ien ; i++) {
                if (i == 0) {
                    numbers.push("previous");
                } else if (i == 6) {
                    numbers.push("next");
                } else {
                    maxpage = Math.ceil(_TotalRecord / gridview.pageSize);
                    pagenum = (Math.ceil(gridview.pageIndex / 5) - 1) * 5 + i;
                    if (pagenum > maxpage) {
                        //当计算出来的页数大于最大页数时跳过
                        continue;
                    } else {
                        numbers.push(pagenum);
                    }

                }
            }

            return numbers;
        }

        //拼接json对象中的数据到模板中去
        function _fnSplicing(_arrDatas) {
            var HtmlDatas = new Array();
            for (var i = 0, ien = _arrDatas.length ; i < ien ; i++) {
                //如果设置了标题长度，则进行缩减
                if (gridview.titleLength > 0) {
                    _arrDatas[i].yoyotui_title = _fnCutTitle(_arrDatas[i].yoyotui_title, gridview.titleLength);
                }

                var tempRow = gridview.template;
                tempRow = tempRow.replace(/yoyotui_title/g, _arrDatas[i].yoyotui_title);
                tempRow = tempRow.replace(/yoyotui_img/g, _arrDatas[i].yoyotui_img);
                tempRow = tempRow.replace(/yoyotui_url/g, _arrDatas[i].yoyotui_url);
                tempRow = tempRow.replace(/yoyotui_disable/g, _arrDatas[i].yoyotui_disable);
                tempRow = tempRow.replace(/yoyotui_lock/g, _arrDatas[i].yoyotui_lock);
                HtmlDatas.push(tempRow);
            }
            return HtmlDatas;
        }

        function _fnGetStrLen(_text) {
            var len = 0;
            for (var i = 0; i < _text.length; i++) {
                if (_text.charCodeAt(i) > 255 || _text.charCodeAt(i) < 0) {
                    len += 2;
                }
                else { len++; }
            }
            return len;
        }

        //截断标题
        function _fnCutTitle(_title, len) {
            var retStr = "";
            var chars = new Array();
            var startp = 0, charlen = 0;
            if (_title.length > len) {
                retStr = _title.substr(0, len) + "...";
            } else {
                retStr = _title;
            }
            return retStr

        }
        //回显处理后的内容到前段页面上
        function _fnRender($obj) {
            //清空Div
            $obj.empty();
            //显示Gridview的内容到页面上
            if (_HtmlGridview.length <= 0) {
                $obj.append(gridview.empty_template);
            } else {
                $obj.append(_HtmlGridview);
                //显示分页的内容到页面上
                _fnCreatePageButton($obj);
            }
        }

        //异步加载数据
        function _fnAjaxLoad($obj) {
            var htmlDatas = new Array();
            //添加必要的页数到ajaxUrl中去
            gridview.ajaxData['start'] = (gridview.pageIndex - 1) * gridview.pageSize;
            gridview.ajaxData['length'] = gridview.pageSize;

            $.ajax({
                url: gridview.ajaxUrl,
                type: gridview.ajaxType,
                cache: false,
                dataType: gridview.dataType,
                data: gridview.ajaxData,
                success: function (json) {
                    //遍历数据项获取具体的内容
                    _HtmlGridview = _fnSplicing(json.data);
                    _TotalRecord = json.recordsTotal;
                },
                complete: function () {
                    //回显处理后的内容到前段页面上
                    _fnRender($obj);
                }
            });


        }


    }));
}(window, document));