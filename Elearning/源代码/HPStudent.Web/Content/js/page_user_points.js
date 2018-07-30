$(function(){  

		$('.panel-body').find('.btn-primary').on('click', function ( e ) {

			var ajaxUrl = "pop-user-points.html?id="+$(this).attr("data-id") +"&"+ Date.now();
            $.get(ajaxUrl,function(html){
				$("#pop_edit").empty();
			    $('#pop_edit').append(html);
			    $('#edit_user_points').modal('show'); 

			});
        });

        $('#ajax_example').on('click', function () {
			Custombox.open({
	            target:     'ajax.html',
	            effect:     effects[Math.floor(Math.random() * effects.length)],
	            overlay:    Math.random() >= 0.5,
	            complete:   function () {
	                $('.modal-ajax').find('.infinite').show();
	            }
	        });
        e.preventDefault();
        	return false;
      });



});  

function showValues() {
      var str = $("#user-points-form").serialize();
      $("#results").text(str);
      $('#edit_user_points').modal('hide'); 

      $.post("./default.aspx", $("#user-points-form").serialize(),
	   function(data){
	   		
	     noty({text: data.text, layout: 'topRight', type: 'success'});
	   }, "json");

}