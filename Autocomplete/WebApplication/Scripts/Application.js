// http://jqueryui.com/autocomplete/#multiple-remote

$(function () {

    function split(val) {
        return val.split(/,\s*/);
    }

    function extractLast(term) {
        return split(term).pop();
    }

    $("#tag-input").bind("keydown", function(event) {
        if (event.keyCode === $.ui.keyCode.TAB && $(this).data("ui-autocomplete").menu.active) {
            event.preventDefault();
        }
    });

    $("#tag-input").autocomplete({
        source: function(request, response) {
            $.getJSON("SearchTags", {
                searchTerm: extractLast(request.term)
            }, response);
        },
        focus: function() {
            return false;
        },
        select: function(event, ui) {
            var terms = split(this.value);
            terms.pop();
            terms.push(ui.item.value);
            terms.push("");
            this.value = terms.join(", ");
            return false;
        },
        autoFocus: true
    });

});