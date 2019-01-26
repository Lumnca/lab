window.onload = function () {
    /* 实现文字 x 轴 循环 */
    window.setInterval(function(){
        let loops = document.getElementsByClassName("loop-text"); //得到 HTMLCollection
        for (let index = 0; index < loops.length ;index ++) {
           let element = loops[index];
           let txt = element.textContent;
           let first = txt.substring(0,1);
           let leave = txt.substring(1);
           element.textContent = leave.concat(first);
        }
    },1000);
};