(function() {
    'use strict';
    angular
        .module('craftStation', [])
        .config(config);

    config.$inject = ['$httpProvider', '$logProvider', '$sceProvider'];

    function config($httpProvider, $logProvider, $sceProvider) {
        $httpProvider.defaults.cache = false;

        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }

        // disable IE ajax request caching
        $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';

        $sceProvider.enabled(false);
    }
})();