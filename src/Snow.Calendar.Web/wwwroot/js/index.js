// 月年改变需请求2次，空白位置未填充。
var date = new Date();
var thisYear = date.getFullYear();
var thisMonth = date.getMonth() + 1;
var thisDate = date.getDate();

var year = parseInt($('#yearSel').val());
var month = parseInt($('#monthSel').val());
var day = thisDate;

var $lastBlock;
$(function () {
    loadContent();
    initEvent();
    initComponent();
});

// 加载主体
function loadContent() {
    $.get('/Home/GetContent',
        { year: year, month: month },
        function (data) {
            document.getElementById('headerHtml').innerHTML = data.header;
            document.getElementById('bodyHtml').innerHTML = data.body;
            if (year === thisYear && month === thisMonth) {
                $('#today_button').click();
            }
        });
}

function loadDay() {
    $.get('/Home/GetDay',
        { year: year, month: month, day: day },
        function (data) {
            var holidaies = new Array();
            var holiday = data.holiday;
            for (var i = 0, max = holiday.length; i < max; i++) {
                holidaies.push('<li><span style="vertical-align: middle;">' + holiday[i] + '</span></li>');
            }
            document.getElementById('day').innerText = data.dayText;
            document.getElementById('china_Dt').innerHTML = data.lunarDateText;
            document.getElementById('worlDay').innerText = data.worlDay;
            document.getElementById('numberDay').innerText = data.numberDay;
            document.getElementById('chinaDay').innerText = data.chinaDay;
            document.getElementById('chinaDay2').innerText = data.lunarDateSexagenary;
            document.getElementById('holiday').innerHTML = holidaies.join(' ');
            document.getElementById('animal').innerText = data.animal;
            document.getElementById('solarConstellation').innerText = data.solarConstellation;
            document.getElementById('lunarConstellation').innerText = data.lunarConstellation;
            document.getElementById('yearNaYinFiveElements').innerText = data.yearNaYinFiveElements;
            document.getElementById('monthNaYinFiveElements').innerText = data.monthNaYinFiveElements;
            document.getElementById('dayNaYinFiveElements').innerText = data.dayNaYinFiveElements;
            document.getElementById('solarPalace').innerText = data.solarPalace;
            document.getElementById('solarPlanet').innerText = data.solarPlanet;
            document.getElementById('solarTermDays').innerText = data.solarTermDays;
            document.getElementById('tCount').innerText = data.subtractDays;
        });
}

// 初始化事件
function initEvent() {
    $('#subMonth').click(function () {
        if (month === 1) {
            year -= 1;
            month = 12;
            $('#yearSel').selectpicker('val', year);
            $('#monthSel').selectpicker('val', month);
        } else {
            month -= 1;
            $('#monthSel').selectpicker('val', month);
        }
    });
    $('#addMonth').click(function () {
        if (month === 12) {
            year += 1;
            month = 1;
            $('#yearSel').selectpicker('val', year);
            $('#monthSel').selectpicker('val', month);
        } else {
            month += 1;
            $('#monthSel').selectpicker('val', month);
        }
    });
    $(document).on('click',
        '.block',
        function (e) {
            $e = $(this);
            if ($e.find('.number').length > 0) {
                day = parseInt($e.find('.number')[0].innerText);
            }
            clickBlock($e);
            //loadDay();
        });
    $('#today_button').click(function () {
        if (thisYear !== year) {
            $('#yearSel').selectpicker('val', thisYear);
        }
        if (thisMonth !== month) {
            $('#monthSel').selectpicker('val', thisMonth);
        }
        toToday();
        clickBlock($($('.today')[0]));
    });
    $('#yearSel').change(function () {
        year = parseInt($(this).val());
        loadContent();
    });
    $('#monthSel').change(function () {
        month = parseInt($('#monthSel').val());
        loadContent();
    });
}

// 初始化组件
function initComponent() {
    $('#yearSel').selectpicker({
        width: 100,
        liveSearch: true
    });
    $('#monthSel').selectpicker({
        width: 100
    });
    $('#holidaySel').selectpicker({
        width: 100
    });
}

// 隐藏今天按钮
function toToday() {
    $('#today_button').css('display', 'none');
    year = thisYear;
    month = thisMonth;
    day = thisDate;
}

function leaveToday() {
    $('#today_button').css('display', '');
}

// 点击格子
function clickBlock($e) {
    if (!$e.is($lastBlock)) {
        $($lastBlock).removeClass("block_click");
        $e.addClass('block_click');
        $lastBlock = $e;
        if (thisYear === year && thisMonth === month && thisDate === day) {
            toToday();
        } else {
            leaveToday('');
        }
        loadDay();
    }
}