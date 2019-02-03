window.onMask = function (title, content) {
    var target = $(".mask");
    target.modal("show");

    $(`.mask .mask-module-title`).text(title);
    $(`.mask .mask-module-content`).text(content);
}
$(function(){
    // 全高度
    function set_layout_height_100(){
        let winHeights =[$(window).height(),$(document).height(),$(document.body).height(),$(document.body).outerHeight(true)];
        var maxHeight = winHeights.sort((one,two)=> one < two)[0];        
        $('.layout-height-100').height(maxHeight);
        return maxHeight;
    }
    $(window).on('resize',null,null,set_layout_height_100);
    set_layout_height_100();  

    $(document).on('DOMSubtreeModified',null,null,set_layout_height_100);

    /* 设置高度 */
    $('[data-line-height]').css("line-height",function(index, val){
        let dom = $(this);
        return dom.attr("data-line-height");
    });

    /* 设置高度 */
    $('[data-height]').height(function(index, height){
        let dom = $(this);
        return `${dom.attr('data-height')}px`;
    });
    /* 设置宽度 */
    $('[data-width]').width(function(index, width){
        let dom = $(this);
        return `${dom.attr('data-width')}px`;
    });
    /* 设置最小宽度 */ 
    $('[data-min-width]').css('min-width',function(index,width){
        return `${$(this).attr('data-min-width')}px`;
    });
    /* 设置最大宽度 */ 
    $('[data-max-width]').css('max-width',function(index,width){
        return `${$(this).attr('data-max-width')}px`;
    });

    /* 设置最小高度 */ 
    $('[data-min-height]').css('min-height',function(index,width){
        return `${$(this).attr('data-min-height')}px`;
    });
    /* 设置最大高度 */ 
    $('[data-max-height]').css('max-height',function(index,width){
        return `${$(this).attr('data-max-height')}px`;
    });


    $('[data-margin-top]').css('margin-top',function(index,width){
        return `${$(this).attr('data-margin-top')}px`;
    });

    $('[data-margin-bottom]').css('margin-bottom',function(index,width){
        return `${$(this).attr('data-margin-bottom')}px`;
    });

    $(window).on('resize',null,null,function(){
        $('[data-height-all]').css('height',function(index,width){
            let val = set_layout_height_100() - $('.admin-narbar-top').height() - 20;
            return `${val}px`;
        });
    });
    
    $('[data-height-all]').css('height',function(index,width){
        let val = set_layout_height_100() - $('.admin-narbar-top').height() - 20;
        return `${val}px`;
    });

    $('#admin-only-show-505').on('click',null,null,function(){
        var content =$('#admin-only-show-505-content');

        if(content.css('display') == "block"){
            content.css("display","none");
        }else{
            content.css("display","block");
        }
        
    });

    $('#admin-only-show-505-content-cancel').on('click',null,null,function(){
        $('#admin-only-show-505-content').css("display","none");
    });
});