(function ($) {

    const KEY = {
        LS_CONTRAST: 'acc_high_contrast',
        LS_TEXTSIZE: 'acc_text_size'
    };
    function anuncio(message) {
        var $ann = $('#acc-announcer');
        if ($ann.length === 0) {
            $ann = $('<div id="acc-announcer" aria-live="polite" role="status"></div>').appendTo('body');
        }
        $ann.text(message);
        console.log('accesibilidad: anuncio ->', message);
    }

    //aplica las configuraciones de accesibilifad guardadas
    function aplicarConfigs() {
        const hc = localStorage.getItem(KEY.LS_CONTRAST) === '1';
        const txt = parseFloat(localStorage.getItem(KEY.LS_TEXTSIZE) || '1');
        $('html').toggleClass('high-contrast', hc);
        $('body').toggleClass('high-contrast', hc);
        if (txt === 1) $('body').css('font-size', ''); else $('body').css('font-size', (txt + 'rem'));
        $('#acc-contrast').attr('aria-pressed', hc ? 'true' : 'false');
    }

    //funcion de alto contraste
    function toggleContrast() {
        const current = localStorage.getItem(KEY.LS_CONTRAST) === '1';
        localStorage.setItem(KEY.LS_CONTRAST, current ? '0' : '1');
        aplicarConfigs();
        anuncio('Modo alto contraste ' + (current ? 'desactivado' : 'activado'));
    }

    function increaseText() {
        var size = parseFloat(localStorage.getItem(KEY.LS_TEXTSIZE) || '1');
        size = Math.min(1.6, +(size + 0.1).toFixed(2));
        localStorage.setItem(KEY.LS_TEXTSIZE, size);
        aplicarConfigs();
        anuncio('Tamaño de texto aumentado');
    }

    function decreaseText() {
        var size = parseFloat(localStorage.getItem(KEY.LS_TEXTSIZE) || '1');
        size = Math.max(0.75, +(size - 0.1).toFixed(2));
        localStorage.setItem(KEY.LS_TEXTSIZE, size);
        aplicarConfigs();
        anuncio('Tamaño de texto disminuido');
    }

    function resetAccessibility() {
        localStorage.removeItem(KEY.LS_CONTRAST);
        localStorage.removeItem(KEY.LS_TEXTSIZE);
        aplicarConfigs();
        anuncio('accesibilidad restablecida');
    }

    // widget control
    function openWidget() {
        $('#acc-widget').addClass('expanded');
        $('#acc-main-btn').attr('aria-expanded', 'true');
        $('#acc-panel').attr('aria-hidden', 'false');
        $('#acc-panel .acc-btn').first().focus();
    }
    function closeWidget() {
        $('#acc-widget').removeClass('expanded');
        $('#acc-main-btn').attr('aria-expanded', 'false');
        $('#acc-panel').attr('aria-hidden', 'true');
        $('#acc-main-btn').focus();
    }
    function toggleWidget() {
        if ($('#acc-widget').hasClass('expanded')) closeWidget(); else openWidget();
    }

    window.__acc = window.__acc || {};
    Object.assign(window.__acc, {
        toggleContrast: toggleContrast,
        aplicarConfigs: aplicarConfigs,
        resetAccessibility: resetAccessibility,
        toggleWidget: toggleWidget
    });

    $(function () {
        aplicarConfigs();

        $('#acc-contrast').on('click', toggleContrast);
        $('#acc-increase').on('click', increaseText);
        $('#acc-decrease').on('click', decreaseText);
        $('#acc-reset').on('click', resetAccessibility);
        $('#acc-skip').on('click', function () { $('#main-content').focus(); });

        $('#acc-main-btn').on('click', function (e) {
            e.stopPropagation();
            toggleWidget();
        });

        $(document).on('click', function (e) {
            if (!$(e.target).closest('#acc-widget').length) {
                closeWidget();
            }
        });

        $(document).on('keydown', function (e) {
            if (e.key === 'Escape') closeWidget();
            if (e.altKey && !e.ctrlKey && !e.shiftKey) {
                switch (e.key.toLowerCase()) {
                    case 'h': e.preventDefault(); toggleContrast(); break;
                    case '+':
                    case '=': e.preventDefault(); increaseText(); break;
                    case '-': e.preventDefault(); decreaseText(); break;
                    case '0': e.preventDefault(); resetAccessibility(); break;
                    case 's': e.preventDefault(); $('#main-content').focus(); break;
                }
            }
        });

        $('#acc-panel .acc-btn').on('keydown', function (e) {
            if (e.key === 'ArrowDown' || e.key === 'ArrowRight') {
                e.preventDefault();
                $(this).closest('.acc-panel-inner').find('.acc-btn').eq(($(this).index() + 1) % $(this).closest('.acc-panel-inner').find('.acc-btn').length).focus();
            } else if (e.key === 'ArrowUp' || e.key === 'ArrowLeft') {
                e.preventDefault();
                var idx = $(this).closest('.acc-panel-inner').find('.acc-btn').index(this);
                var prev = (idx - 1 + $(this).closest('.acc-panel-inner').find('.acc-btn').length) % $(this).closest('.acc-panel-inner').find('.acc-btn').length;
                $(this).closest('.acc-panel-inner').find('.acc-btn').eq(prev).focus();
            }
        });

    });

})(jQuery);