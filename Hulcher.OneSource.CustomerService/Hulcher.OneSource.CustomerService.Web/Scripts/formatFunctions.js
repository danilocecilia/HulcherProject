function formatCurrency(num, control) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));
    if (control != null) {
        control.value = (((sign) ? '' : '-') + '$' + num + '.' + cents);
    }
    return (((sign) ? '' : '-') + '$' + num + '.' + cents);
}

function formatDecimal(num, control) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));
    if (control != null) {
        control.value = (((sign) ? '' : '-') + num + '.' + cents);
    }
    return (((sign) ? '' : '-') + num + '.' + cents);
}

function formatDecimalOO(num, control) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));
    if (control != null) {
        if (num < 10)
            control.value = (((sign) ? '' : '-') + "0" + num + '.' + cents);
        else
            control.value = (((sign) ? '' : '-') + num + '.' + cents);
    }

    if (num < 10)
        return (((sign) ? '' : '-') + "0" + num + '.' + cents);
    else    
        return (((sign) ? '' : '-') + num + '.' + cents);
}

function formatDecimalWithoutSeparator(num, control) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    //    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
    //        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
    //            num.substring(num.length - (4 * i + 3));
    if (control != null) {
        control.value = (((sign) ? '' : '-') + num + '.' + cents);
    }
    return (((sign) ? '' : '-') + num + '.' + cents);
}

function formatPercent(num, control) {
    return formatDecimal(num, control) + "%";
}

function formatInteger(num) {
    num = parseInt(num.toString());
    if (isNaN(num))
        num = "0";
    return num;
}

function formatLonLat(event) {
    event = event || window.event;
    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode == 46 || charCode == 37 || charCode == 39)
        return true;
    if (event.srcElement.value.length == 0 && charCode == 189)
        return true;
    if (event.srcElement.value.length >= 12 && charCode > 31)
        return false;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode != 190 || (charCode == 190 && event.srcElement.value.indexOf('.') != -1)))
        return false;

    return true;
};