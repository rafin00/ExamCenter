$(document).ready(function () {
    $('#btn').on('click', function () {
        var prices = [];
        
        $('input:checked').each(function () {
            prices.push($(this).attr("value"));
           // alert($(this).attr("value"));

        });
        $.ajax({
            url: 'AssignStudent',
            type: "POST",
            data: { values: prices },
            dataType: "json",
            traditional: true,
            success: function () {
              //  alert("ajax request to server succeed");
            }
        });
    });

    $('#asbtn').on('click', function () {
        var prices = [];
        var crs = $('#dropDown0').val();

        $('input:checked').each(function () {
            prices.push($(this).attr("value"));
           // alert($(this).attr("value"));

        });
        $.ajax({
            url: 'SelectQuestions',
            type: "POST",
            data: { values: prices },
            dataType: "json",
            traditional: true,
            success: function () {
             //   alert("ajax request to server succeed");
            }
        });
    });
    
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return decodeURI(results[1]) || 0;
        }
    }

        $("input:radio").change(function () {
            var n = this.name;
            var v = this.value;
           
            var p = $.urlParam('evnt_id');
            var c = $.urlParam('course_id');
           
           // alert(n + v +p +c);
            $.ajax({ 
                url: 'SaveAnswers',
                type: "GET",
                data: { id: n, answer: v, para:p,crs:c },
                dataType: "json",
                traditional:true
            });
     });
   
   
    
})

