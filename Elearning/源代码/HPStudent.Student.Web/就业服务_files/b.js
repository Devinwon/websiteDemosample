var BDBridgeConfig=(function(){var self;var arrPQiao=["http:\/\/p.qiao.baidu.com\/","http:\/\/p0.qiao.baidu.com\/","http:\/\/p1.qiao.baidu.com\/","http:\/\/p2.qiao.baidu.com\/","http:\/\/p3.qiao.baidu.com\/","http:\/\/p4.qiao.baidu.com\/","http:\/\/p5.qiao.baidu.com\/","http:\/\/p6.qiao.baidu.com\/","http:\/\/p7.qiao.baidu.com\/","http:\/\/p8.qiao.baidu.com\/","http:\/\/p9.qiao.baidu.com\/"];var randomUrl=function(array){var tag=Math.floor(Math.random()*array.length);return array[tag];};var shuffle=function(){var tag=Math.floor(Math.random()*arrPQiao.length);var url=arrPQiao[tag];if(arrPQiao.length>1){arrPQiao.splice(tag,1);}
return url;}
var CONFIG={BD_BRIDGE_OPEN:1,BD_BRIDGE_ROOT:randomUrl(["http:\/\/qiao.baidu.com\/v3\/"]),BD_BRIDGE_RCV_ROOT:shuffle(),BD_BRIDGE_IM_ROOT:shuffle(),VERSION:"3.0.0"};if(BDBridgeConfig&&BDBridgeConfig.VERSION>=CONFIG.VERSION){return BDBridgeConfig;}
if(CONFIG.BD_BRIDGE_OPEN==1){var script=document.createElement("script");script.type="text/javascript";script.charset="UTF-8";script.src=CONFIG.BD_BRIDGE_ROOT+"asset/front/bsl.js?t="+(+new Date());document.getElementsByTagName("head")[0].appendChild(script);}
var timeStart=new Date().getTime();return self={TIME_START:timeStart,VERSION:CONFIG.VERSION,OPEN:CONFIG.BD_BRIDGE_OPEN,ROOT:CONFIG.BD_BRIDGE_ROOT,RCV_ROOT:CONFIG.BD_BRIDGE_RCV_ROOT,IM_ROOT:CONFIG.BD_BRIDGE_IM_ROOT,BD_BRIDGE_DATA:{mainid:"121069414",SITE_ID:"2b284ace4ec0b4d36f29fd3cf9e2e598",siteid:"6371502",ucid:"8143686",userName:'厚溥教育027:易超群'},OPEN_MODULES:"00000",JS_LOADER_URL:CONFIG.BD_BRIDGE_ROOT+'asset/front/entry/',CSS_DEFAULT_URL:'http://s.qiao.baidu.com/asset/front/css/default/',CSS_LOADER_URL:"http://s.qiao.baidu.com/style/414/121069414/",BD_BRIDGE_SPECIAL:[],BD_BRIDGE_STYLE_ITEM:[{pageid:"0",styleid:"1"-0,BD_BRIDGE_GROUP:['0'-0,'1'-0],BD_BRIDGE_ICON:{iconlevel:"2"-0,icontype:"1"-0,iconposition:{postype:"1"-0,position:"1"-0},iconskin:{useOfflineimg:"0"-0},iconmode:"0"-0},BD_BRIDGE_INVITE:{text:"<p>您好，欢迎来到美国硅谷软件学院！以最专业的技术服务，提供给您最好的学习体验！欢迎您的咨询！<\/p>",type:'0',way:("1"*Math.pow(2,0))+("1"*Math.pow(2,1)),page:'1'-0,interval:'30'-0,time:'5'-0},BD_BRIDGE_WEBIM:{webimtype:'1'-0,webimchat:{showtype:'2'-0,name:''},webimposition:'2'-0,webimlitebgcolor:'#d5d5d5'},BD_BRIDGE_MESS:{messItem:{messItemName:'0'-0,messItemPhone:'1'-0,messItemAddress:'0'-0,messItemEmail:'0'-0,messItemText:'1'-0,language:'0'-0},messType:{disableMess:'0'-0},messShow:{messFloatType:'0'-0},extraMessItems:[{key:"QQ",necessity:1}]}}]}})();