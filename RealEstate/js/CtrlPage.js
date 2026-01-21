var PageControl = jQuery.Class.create(
{
    init: function(ImgLoading, ClsName, CurrentPage, PageUrl, DivUpdate) {
        this.ImgLoading = ImgLoading;
        this.ClsName = ClsName;
        this.CurrentPage = CurrentPage;
        this.PageUrl = PageUrl;
        this.DivUpdate = DivUpdate;
        this.SetControlEvent();
    },

    AjaxCall: function() {
        var strconcat = '&';
        if (this.PageUrl.indexOf('?') == -1)
            strconcat = '?';
        AjaxRequest(this.ImgLoading, this.DivUpdate, this.PageUrl + strconcat + "page=" + this.CurrentPage + "&call=ajax", 'POST');
    },

    SetControlEvent: function() {
        var ClientID = this.ClsName.replace('.pagelnk', '');
        if (document.getElementById('HrefPrev' + ClientID) != null) {
            $('#HrefPrev' + ClientID).click(function() {
                var TmpClID = this.id.replace('HrefPrev', '');
                var CurrPage = parseInt(eval('Obj' + TmpClID + '.CurrentPage'));
                eval('Obj' + TmpClID + '.CurrentPage= ' + (CurrPage - 1) + ';Obj' + TmpClID + '.AjaxCall();');
            });
        }

        $(this.ClsName).each(function(Index) {
            $(this).click(function() {
                var TmpClID = $(this).attr('class').replace('pagelnk', '');
                eval('Obj' + TmpClID + '.CurrentPage= ' + parseInt($(this).html()) + ';Obj' + TmpClID + '.AjaxCall();');
            });
        });

        if (document.getElementById('HrefNext' + ClientID) != null) {
            $('#HrefNext' + ClientID).click(function() {
                var TmpClID = $(this).attr('id').replace('HrefNext', '');
                var CurrPage = parseInt(eval('Obj' + TmpClID + '.CurrentPage'));
                eval('Obj' + TmpClID + '.CurrentPage= ' + (CurrPage + 1) + ';Obj' + TmpClID + '.AjaxCall();');
            });
        }
    }
});

/* Form post pagging */ 
var CurrentPage=1;
function PostPageForm(ctrl,type)
{        
    if(type=='c')               
        CurrentPage=parseInt(ctrl.innerHTML);                    
    else
        if(type=='n')            
            CurrentPage= parseInt($('hdnPage').value) + 1;                                            
        else            
            CurrentPage= parseInt($('hdnPage').value) - 1;                
        
    $('hdnPage').value=CurrentPage;
    $('btnPageSubmit').click();
}
/* End Here */ 