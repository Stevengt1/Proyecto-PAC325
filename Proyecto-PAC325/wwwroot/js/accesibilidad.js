(function ($) {

    const KEY = {
        LS_CONTRAST: 'acc_high_contrast',
        LS_TEXTSIZE: 'acc_text_size'
    };

    function announce(message) {
        var $ann = $('#acc-announcer');
        if ($ann.length === 0) {
            $ann = $('<div id="acc-announcer" aria-live="polite" role="status"></div>').appendTo('body');
        }
        $ann.text(message);
    }

    function applicarAlto() {
        const hc = localStorage.getItem(KEY.LS_CONTRAST) === '1';
        const txt = parseFloat(localStorage.getItem(KEY.LS_TEXTSIZE) || '1');
        $('html').toggleClass('high-contrast', hc);
        $('body').toggleClass('high-contrast', hc);
        $('body').css('font-size', (txt === 1 ? '' : (txt + 'rem')));
    }

    // aplicar configuraciones guardadas
    function toggleContrast() {
        const current = localStorage.getItem(KEY.LS_CONTRAST) === '1';
        localStorage.setItem(KEY.LS_CONTRAST, current ? '0' : '1');
        applicarAlto();
        //mensaje que se va anunciar en el footer
        announce('Modo alto contraste ' + (current ? 'desactivado' : 'activado'));
    }

    //incrementar y disminuir texto
    function increaseText() {
        var size = parseFloat(localStorage.getItem(KEY.LS_TEXTSIZE) || '1');
        size = Math.min(1.5, +(size + 0.1).toFixed(2));
        localStorage.setItem(KEY.LS_TEXTSIZE, size);
        applicarAlto();
        announce('Tamaño de texto aumentado');
    }
    function decreaseText() {
        var size = parseFloat(localStorage.getItem(KEY.LS_TEXTSIZE) || '1');
        size = Math.max(0.85, +(size - 0.1).toFixed(2));
        localStorage.setItem(KEY.LS_TEXTSIZE, size);
        applicarAlto();
        announce('Tamaño de texto disminuido');
    }

    //opcion de reset
    function resetAccessibility() {
        localStorage.removeItem(KEY.LS_CONTRAST);
        localStorage.removeItem(KEY.LS_TEXTSIZE);
        applicarAlto();
        announce('Configuración de accesibilidad restablecida');
    }

    //cuando se usa tab se puede usar el teclado
    var lastKey;
    $(window).on('keydown', function (e) {
        lastKey = e.key;
        if (e.altKey && !e.shiftKey && !e.ctrlKey) {
            switch (e.key.toLowerCase()) {
                //alt+ h / + / - / 0
                case 'h': e.preventDefault(); toggleContrast(); break;
                case '+': e.preventDefault(); increaseText(); break;
                case '-': e.preventDefault(); decreaseText(); break;
                case '0': e.preventDefault(); resetAccessibility(); break;
            }
        }
    });

    $(window).on('keyup', function () { lastKey = null; });

    $(document).on('keydown', function (e) {
        if (e.key === 'Tab') {
            $('body').addClass('keyboard-nav');
        }
    });

    //acciones de los botones de accesibilidad
    $(function () {
        applicarAlto();

        $('#acc-contrast').on('click', toggleContrast);
        $('#acc-increase').on('click', increaseText);
        $('#acc-decrease').on('click', decreaseText);
        $('#acc-reset').on('click', resetAccessibility);

        $('#acc-contrast').attr('title', 'Alternar alto contraste (Alt+H)');
        $('#acc-increase').attr('title', 'Aumentar texto (Alt+=)');
        $('#acc-decrease').attr('title', 'Disminuir texto (Alt+-)');
        $('#acc-reset').attr('title', 'Restablecer accesibilidad (Alt+0)');
        $('#acc-skip').on('click', function () { $('#main-content').focus(); });

        $('.acc-btn').attr('tabindex', '0').each(function () {
            var $t = $(this);
            if (!$t.attr('aria-label')) $t.attr('aria-label', $t.text());
        });
    });

})(jQuery);