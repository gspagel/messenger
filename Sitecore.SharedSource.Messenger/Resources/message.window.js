$.noConflict();

var messenger = new function () {
    this.createContainer = function () {
        var container = document.createElement('div');

        container.setAttribute('id', 'messengerContainer');

        container.style.display = 'none';
        container.style.backgroundColor = '#FFF';
        container.style.border = '2px solid #999';
        container.style.padding = '10px';

        container.style.position = 'absolute';
        container.style.zindex = '99999';
        container.style.bottom = '0';
        container.style.right = '45px';

        container.style.width = '400px';
        container.style.height = '200px';
        container.style.overflow = 'scroll';

        document.body.appendChild(container);
    };

    this.getMessages = function () {
        var message = '';

        setTimeout(function () {
            jQuery.ajax({
                type: 'GET',
                dataType: 'json',
                url: '/MessageService.svc/GetMessage',
                timeout: 15000,
                success: function (data) {
                    if (data.MessageText !== '') {
                        if (data.MessageTitle != '') {
                            message += '<h1>' + data.MessageTitle + '</h1>';
                        }

                        message += '<p>' + data.MessageText + '</p>';

                        jQuery('#messengerContainer').html(message);
                        jQuery('#messengerContainer').show();
                    }
                },
                error: function (result) {
                    alert('Service call failed: ' + result.status + ' ' + result.statusText);
                }
            });
        }, 15000);
    };
};

jQuery(document).ready(function () {
    messenger.createContainer();
    messenger.getMessages();
});
