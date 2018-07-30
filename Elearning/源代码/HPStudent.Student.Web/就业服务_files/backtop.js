function svse(){
	this.init();
}
svse.prototype = {
	constructor: svse,
	init: function(){		
		this._initBackTop();
	},	
	_initBackTop: function(){
		var $backTop = this.$backTop = $('<div class="cbbfixed">'+
						'<a class="a cbbtn">'+
							'<span class="a-icon"></span><div></div>'+
						'</a>'+
						'<a class="b cbbtn">'+
							'<span class="b-icon"></span><div></div>'+
						'</a>'+
						'<a class="c cbbtn">'+
							'<span class="c-icon"></span><div></div>'+
						'</a>'+
						'<a class="d cbbtn">'+
							'<span class="d-icon"></span>'+
						'</a>'+
					'</div>');
		$('body').append($backTop);
		
		$backTop.click(function(){
			$("html, body").animate({
				scrollTop: 0
			}, 100);
		});
		var timmer = null;
		$(window).bind("scroll",function() {
            var d = $(document).scrollTop(),
            e = $(window).height();
            0 < d ? $backTop.css("bottom", "100px") : $backTop.css("bottom", "100px");
			clearTimeout(timmer);
			timmer = setTimeout(function() {
                clearTimeout(timmer)
            },100);
	   });
	}
	
}
var svse = new svse();