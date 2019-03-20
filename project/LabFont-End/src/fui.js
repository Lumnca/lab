var fui = {};

fui.Terminal = function () {
    var ua = navigator.userAgent,
        isWindowsPhone = /(?:Windows Phone)/.test(ua),
        isSymbian = /(?:SymbianOS)/.test(ua) || isWindowsPhone,
        isAndroid = /(?:Android)/.test(ua),
        isFireFox = /(?:Firefox)/.test(ua),
        isChrome = /(?:Chrome|CriOS)/.test(ua),
        isTablet = /(?:iPad|PlayBook)/.test(ua) || (isAndroid && !/(?:Mobile)/.test(ua)) || (isFireFox &&
            /(?:Tablet)/.test(ua)),
        isPhone = /(?:iPhone)/.test(ua) && !isTablet,
        isPc = !isPhone && !isAndroid && !isSymbian;
    return {
        isTablet: isTablet,
        isPhone: isPhone,
        isAndroid: isAndroid,
        isPc: isPc
    };
};

fui.IsEmail = function(email) {
    var myReg = /^[a-zA-Z0-9_-]+@([a-zA-Z0-9]+\.)+(com|cn|net|org)$/;
    return myReg.test(email);
}

window.f = fui;

/**
 * @param { Date } date
 */
function Format(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();

    return `${year}/${month}/${day} 00:00`
}
/**
* @param { Date } date
* @param { Number } last
* @return { Date }
*/
function getLastDay(date ,last) {
    var $date = new Date(date.getTime());
    $date.setDate($date.getDate() - last);
    return $date;
}


/**
* @param { Date } date
* @param {Number} length
*/
function getLastMonth(date,length) {
    for(let i = 0; i<length; i++){
        var $date =  getLastDay(date,i);
        var format = Format($date);
        console.log(format);
    }
}

fui.Format = Format;
fui.getLastDay = getLastDay;
fui.getLastMonth = getLastMonth;