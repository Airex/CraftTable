var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }


//
// XIVDB tooltips
//

var XIVDBTooltipsClass = (function () {
    function XIVDBTooltipsClass() {
        _classCallCheck(this, XIVDBTooltipsClass);

        this.version = 1;

        // timer delay
        this.tooltipsTimer = false;
        this.tooltipsDelay = 2500;

        // tooltips queue
        this.queueTimer = false;
        this.queueDelay = 500;

        // vars
        this.onMobile = false;
        this.startTimestamp = null;

        
     
    }

    //
    // XIVDB tooltips
    //

    _createClass(XIVDBTooltipsClass, [{
        key: 'init',
        value: function init() {
            var _this = this;

            this.startTimestamp = new Date();

            // validate tooltips
            this.validateTooltipOptions();

            // check for jquery then run get
            XIVDBTooltipsDependency.pass(function () {
                // mobile check
                _this.onMobile = $(window).width() <= _this.getOption('mobileMinimumWidth') ? true : false;

                // prepare
                XIVDBTooltipsDOM.prepare();

                // get tooltips
                _this.get();
            });
        }
    }, {
        key: 'get',
        value: function get() {
            var _this2 = this;

            this.startTimestamp = new Date();

            clearTimeout(this.queue);
            this.queue = setTimeout(function () {
                // detect links
                XIVDBTooltipsLinks.detect();
                var links = XIVDBTooltipsLinks.links;

                if (links) {
                    _this2.query(links, function (tooltips) {
                        // tooltip completed event
                        if (CompletedEvent = XIVDBTooltips.getOption('event_tooltipsLoaded')) {
                            CompletedEvent(tooltips);
                        }

                        // inject tooltips into dom
                        XIVDBTooltipsDOM.inject(tooltips);
                    });
                }
            }, this.queueDelay);
        }

        //
        // Query for tooltips
        //
    }, {
        key: 'query',
        value: function query(links, callback) {
            var list = {};

            // organize all links
            links.each(function (i, element) {
                var original = $(element).attr('data-xivdb-key'),
                    isset = $(element).attr('data-xivdb-isset'),
                    key = original.split('_'),
                    type = key[1].toString(),
                    id = parseInt(key[2]);

                // if key already processed, don't query it
                if (XIVDBTooltipsHolder.exists(original) && isset) {
                    return;
                }

                // if empty for whatever reason..
                if (type.length == 0 || !id) {
                    return;
                }

                // if type does not exist in list, add it
                if (typeof list[type] === 'undefined') {
                    list[type] = [];
                }

                if (list[type].indexOf(id) == -1) {
                    // add to list
                    list[type].push(id);
                }
            });

            // join all lists
            for (var i in list) {
                list[i] = list[i].join(',');
            }

            // query tooltips
            if (Object.keys(list).length > 0) {
                // query tooltips
                $.ajax({
                    url: XIVDBTooltips.getOption('source') + '/tooltip?t=' + Date.now(),
                    method: 'POST',
                    dataType: 'json',
                    cache: true,
                    data: {
                        list: list,
                        language: this.getOption('language')
                    },

                    // on success
                    success: function success(tooltips) {
                        // if empty
                        if (!tooltips) {
                            console.error('Tooltips returned no response');
                            return;
                        }

                        // if not an object
                        if (typeof tooltips !== 'object') {
                            console.error('Tooltips response was not an object');
                            return;
                        }

                        // callback
                        if (callback) {
                            callback(tooltips);
                        }
                    },

                    // on error
                    error: function error(data, status, code) {
                        console.error('----------------');
                        console.error('Error loading tooltips!');
                        console.error(data.responseText);
                        console.error(status, code);
                        console.error('----------------');
                    }
                });
            }
        }

        //
        // Get delayed
        //
    }, {
        key: 'getDelayed',
        value: function getDelayed() {
            clearTimeout(this.tooltipsTimer);
            this.tooltipsTimer = setTimeout(function () {
                XIVDBTooltips.get();
            }, this.tooltipsDelay);
        }

        //
        // Hide tooltips
        //
    }, {
        key: 'hide',
        value: function hide() {
            XIVDBTooltipsDOM.hide();
        }

        // ------------------------------------------------------------------

        //
        // Get a cache time
        //
    }, {
        key: 'cachetime',
        value: function cachetime() {
            var date = new Date();
            return '' + date.getFullYear() + date.getDate();
        }

        //
        // Set the options
        //
    }, {
        key: 'setOptions',
        value: function setOptions(options) {
            // Options passed
            this.options = options;

            // if window location is on xivdb, then remove credit
            if (window.location.href.indexOf(this.xivdb) > -1) {
                this.options.includeCredits = false;
            }

            return this;
        }

        //
        // Get an option
        //
    }, {
        key: 'getOption',
        value: function getOption(setting) {
            return this.options[setting];
        }

        //
        // Verifies tooltips options and sets the default on missing ones
        //
    }, {
        key: 'validateTooltipOptions',
        value: function validateTooltipOptions() {
            for (var option in xivdb_tooltips_default) {
                if (typeof this.options[option] === 'undefined') {
                    this.options[option] = xivdb_tooltips_default[option];
                }
            }
        }
    }]);

    return XIVDBTooltipsClass;
})();

var XIVDBTooltipsDependencyClass = (function () {
    function XIVDBTooltipsDependencyClass() {
        _classCallCheck(this, XIVDBTooltipsDependencyClass);

        // Version of jquery to include
        this.jquery = 'https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js';
    }

    //
    // Tooltip DOM class
    //

    _createClass(XIVDBTooltipsDependencyClass, [{
        key: 'pass',
        value: function pass(callback) {
            var _this3 = this;

            if (typeof jQuery !== 'undefined') {
                return callback();
            }

            setTimeout(function () {
                if (typeof jQuery === 'undefined') {
                    _this3.loadScript(_this3.jquery, function () {
                        if (!$) {
                            var $ = jQuery;
                        }
                        return callback();
                    });
                }
            }, XIVDBTooltips.getOption('jqueryCheckDelay'));
        }
    }, {
        key: 'loadScript',
        value: function loadScript(url, callback) {
            var script = document.createElement("script");
            script.type = "text/javascript";

            if (script.readyState) {
                script.onreadystatechange = function () {
                    if (script.readyState === "loaded" || script.readyState === "complete") {
                        script.onreadystatechange = null;
                        callback();
                    }
                };
            } else {
                script.onload = function () {
                    callback();
                };
            }

            script.src = url;
            document.getElementsByTagName('head')[0].appendChild(script);
        }
    }]);

    return XIVDBTooltipsDependencyClass;
})();

var XIVDBTooltipsDOMClass = (function () {
    function XIVDBTooltipsDOMClass() {
        _classCallCheck(this, XIVDBTooltipsDOMClass);

        this.isActive = false;
    }

    //
    // Tooltips DOM class
    //

    // get the site ready for tooltips

    _createClass(XIVDBTooltipsDOMClass, [{
        key: 'prepare',
        value: function prepare() {
            var _this4 = this;

            var source = XIVDBTooltips.getOption('source'),
                cachetime = XIVDBTooltips.cachetime();

            // add the tooltip
            $('head').append('<link href="' + source + '/tooltips.css?v=' + cachetime + '" rel="stylesheet" type="text/css">');

            if (!XIVDBTooltips.onMobile) {
                // append tooltip placeholder
                $('body').append('<div class="xivdb" style="display:none;"></div>');

                $('html').on('keyup keydown',
                    null,
                    function(event) {
                        _this4.ctrlActive = event.ctrlKey;
                        if (_this4.ctrlActive)
                            _this4.show(event);
                        else
                            _this4.hide(event);
                    });

                // on mouse entering a tooltip
                $('html').on('mouseenter', '[data-xivdb-key]', function (event) {
                    _this4.isActive = true;
                    var $element = $(event.currentTarget),
                        key = $element.attr('data-xivdb-key'),
                        html = XIVDBTooltipsHolder.get(key);

                    // if element is being dragged, don't show any tooltips
                    if ($('.ui-draggable-dragging').length > 0) {
                        return;
                    }

                    // set the html into the xivdb placeholder
                    $('.xivdb').html(html);

                    // show the tooltip
                    if (!_this4.ctrlActive) return;
                    _this4.show(event);
                }).on('mouseleave', '*[data-xivdb-key]', function (event) {
                    _this4.isActive = false;
                    var $element = $(event.target);
                    $('.xivdb').empty();
                    // hide tooltips
                    _this4.hide();
                });

                // on mouse move
                $('html').on('mousemove', function (event) {
                    if (_this4.isActive) {
                        _this4.follow(event);
                    }
                });

                // set 100%
                if (!XIVDBTooltips.getOption('preventHtmlHeight')) {
                    $('html').css({ 'height': '100%' });
                }
            }
        }

        //
        // Inject tooltips
        //
    }, {
        key: 'inject',
        value: function inject(tooltips) {
            // loop through types
            for (var type in tooltips) {
                // go through all tooltips
                for (var i in tooltips[type]) {
                    // get tooltip
                    var tooltip = tooltips[type][i];
                    if (!tooltip) {
                        console.log('false tooltip');
                        continue;
                    }

                    var data = tooltip.data,
                        html = tooltip.html,
                        key = 'xivdb_' + type + '_' + data.id,
                        $atags = $('[data-xivdb-key="' + key + '"]');

                    // inject tooltip
                    XIVDBTooltipsHolder.add(key, html);

                    // --------------------------------------------------------------

                    var $link = $atags;
                    $link.attr('data-xivdb-isset', 1);

                    // if attribute: data-xivdb-replace="0" is set, don't replace anything.
                    if (this.checkLinkSettings($link, ['replace'])) {
                        // if to replace name or not
                        if (this.checkLinkSettings($link, ['seturlname', 'replacename'])) {
                            $link.text(data.name);
                        }

                        // if to color name or not
                        if (this.checkLinkSettings($link, ['seturlcolor', 'colorname'])) {
                            var css = XIVDBTooltips.getOption('seturlcolorDarken') ? 'rarity-' + data.color + '-darken' : 'rarity-' + data.color;

                            $link.addClass(css);
                        }

                        // if to set icon or not
                        if (this.checkLinkSettings($link, ['seturlicon', 'showicon'])) {
                            var css = 'xivdb-ii',
                                iconsize = parseInt($('a[data-xivdb-key]').css('font-size'), 10) + 4;

                            if (data.icon) {
                                if (data.icon.indexOf('finalfantasyxiv.com') == -1 &&
                                    data.icon.indexOf('secure.xivdb') == -1) {
                                    data.icon = 'https://secure.xivdb.com' + data.icon;
                                }
                            }

                            $link.prepend('<img src="' + data.icon + '" style="height:' + iconsize + 'px;" class="' + css + '">');
                        }
                    }
                }
            }
        }

        //
        // Check the settings on a link
        //
    }, {
        key: 'checkLinkSettings',
        value: function checkLinkSettings($link, settings) {
            // go through each setting, if any fail, then it cannot pass!
            for (var i in settings) {
                var option = settings[i],
                    attr = 'data-xivdb-' + option;

                // IF the settings are false, then pass is false
                if (XIVDBTooltips.getOption(option) === false) {
                    return false;
                }

                // if option "replace" exists, ignore completely
                if ($link.attr('data-xivdb-replace')) {
                    return false;
                }

                // IF the attribute is not undefined
                // AND the attribute is false
                if (typeof $link.attr(attr) !== 'undefined' && ($link.attr(attr) == '0' || $link.attr(attr).toLowerCase() == 'false')) {
                    return false;
                }
            }

            return true;
        }

        //
        // Hide the tooltip
        //
    }, {
        key: 'hide',
        value: function hide() {
            $('.xivdb').hide();
        }

        //
        // Show the tooltip
        //
    }, {
        key: 'show',
        value: function show(event) {
            // follow an event
            this.follow(event);

            // show tooltip
            $('.xivdb').show();
        }

        //
        // Follow mouse
        //
    }, {
        key: 'follow',
        value: function follow(event) {
            // Get x/y position and page positions
            var left = event.pageX + XIVDBTooltips.getOption('tooltipDistanceX'),
                top = event.pageY + XIVDBTooltips.getOption('tooltipDistanceY'),
                width = $('.xivdb').outerWidth(true),
                height = $('.xivdb').outerHeight(true),
                topOffset = top + height,
                leftOffset = left + width,
                topLimit = $(window).height() + $(window).scrollTop(),
                leftLimit = $(window).scrollLeft() + $(window).width();

            // Positions based on window boundaries
            if (leftOffset > leftLimit) left = event.pageX - (width + XIVDBTooltips.getOption('tooltipDistanceX'));
            if (topLimit < topOffset) top = event.pageY - (height + XIVDBTooltips.getOption('tooltipDistanceY'));

            // tell tooltip to position somewhere
            this.position(left, top);
        }

        //
        // Position the tooltip somewhere
        //
    }, {
        key: 'position',
        value: function position(left, top) {
            $('.xivdb').css({
                top: top + 'px',
                left: left + 'px'
            });
        }
    }]);

    return XIVDBTooltipsDOMClass;
})();

var XIVDBTooltipsHolderClass = (function () {
    function XIVDBTooltipsHolderClass() {
        _classCallCheck(this, XIVDBTooltipsHolderClass);

        this.tooltips = {};
    }

    //
    // Handle links
    //

    _createClass(XIVDBTooltipsHolderClass, [{
        key: 'add',
        value: function add(id, html) {
            this.tooltips[id] = html;
        }
    }, {
        key: 'get',
        value: function get(id) {
            return this.tooltips[id];
        }
    }, {
        key: 'exists',
        value: function exists(id) {
            return this.tooltips[id] ? true : false;
        }
    }]);

    return XIVDBTooltipsHolderClass;
})();

var XIVDBTooltipsLinksClass = (function () {
    function XIVDBTooltipsLinksClass() {
        _classCallCheck(this, XIVDBTooltipsLinksClass);

        this.links = null;
    }

    //
    // Detect links on the page
    //

    _createClass(XIVDBTooltipsLinksClass, [{
        key: 'detect',
        value: function detect() {
            // tooltip container
            var container = XIVDBTooltips.getOption('linkContainer');

            $('a, *[data-tooltip-id]').each(function (i, element) {
                var $link = $(element),
                    href = $link.attr('href'),
                    dataId = $link.attr('data-tooltip-id'),
                    type = false,
                    id = false;

                if (!dataId) {
                    // Skip if:
                    // 	 if ignored
                    // 	 undefined
                    // 	 or too short
                    if ($link.attr('data-xivdb-ignore') || typeof href === 'undefined' || href.length < 1) {
                        return;
                    }
                }

                // ignore if already processed before
                if ($link.attr('data-xivdb-tooltip')) {
                    return;
                }

                // check for hidden link condition
                if (!XIVDBTooltips.getOption('includeHiddenLinks') && !$link.is(':visible')) {
                    return;
                }

                if (!dataId) {
                    // remove any double slashes
                    href = href.toString().toLowerCase().replace('http://', '').replace('https://', '').replace('//', '/').replace('?', '').toString();

                    // is the link not an XIVDB link (and not local)
                    if (href[0] != '/' && href.indexOf('xivdb.com') == -1) {
                        return;
                    }

                    // remove url
                    href = href.replace('fr.', '').replace('de.', '').replace('ja.', '').replace('cns.', '').replace('www.', '');
                    href = href.replace('xivdb.com', '');

                    // split up the link and clean it
                    href = href.split('/').filter(function (n) {
                        return n.toString().length > 0;
                    });

                    // does a valid type exist
                    if (xivdb_tooltips_valid_types.indexOf(href[0]) == -1) {
                        return;
                    }

                    // get type and ID
                    type = href[0];
                    id = href[1];

                    // if url length below two, it isn't valid, as
                    // 2 = TYPE and ID
                    if (typeof href == 'undefined' || href.length < 2) {
                        return;
                    }
                } else {
                    dataId = dataId.split('/');

                    type = dataId[0];
                    id = dataId[1];
                }

                // create a sort of cache key
                var key = 'xivdb_' + type + '_' + id;

                // attach the key to the element, (or the parent element)
                // we also check we havent added the key before
                if ($link.attr('data-xivdb-parent') && !$link.attr('data-xivdb-key')) {
                    // add the key to the parent element
                    return $link.parents($link.attr('data-xivdb-parent')).attr('data-xivdb-key', key);
                } else if (!$link.attr('data-xivdb-parent')) {
                    // add the key to the element
                    return $link.attr('data-xivdb-key', key);
                }
            });

            // links will be anything with xivdb key
            this.links = $('[data-xivdb-key]');
        }
    }]);

    return XIVDBTooltipsLinksClass;
})();

var xivdb_tooltips_default = {
    //
    // Settings
    //

    // Where to get tooltips from.
    source: 'https://secure.xivdb.com',

    // Language the tooltips should be in
    language: 'en',

    // Should tooltips attempt to replace the link?
    // if set to false: seturlname, seturlcolor and seturlicon will be skipped
    replace: true,

    // Should tooltips replace the name of the link?
    seturlname: true,

    // Should tooltips apply a rarity color? (eg: Relics set to Purple)
    seturlcolor: true,

    // If your site is white/bright, set this true
    seturlcolorDarken: true,

    // Should tooltips include an icon?
    seturlicon: true,

    // Should tooltips replace hidden links?
    includeHiddenLinks: false,

    // How long to wait until attempting to include jquery
    // if jquery still doesn't exist after this time, it will
    // be auto included
    jqueryCheckDelay: 500,

    // Minimum site width (pixels) to assume we're on a mobile,
    // tooltips should not be used on mobile sites.
    mobileMinimumWidth: 500,

    // Prevent this script from setting your sites html height to 100%
    // this helps with knowing mouse position.
    preventHtmlHeight: false,

    // How far the tooltip should be from the mouse
    tooltipDistanceX: 20,
    tooltipDistanceY: 20,

    //
    // EVENTS
    //

    // this is called once tooltips load, provides tooltip data,
    // eg: event_tooltipsLoaded: function(tooltips) { ... }
    event_tooltipsLoaded: null
};

var xivdb_tooltips_valid_types = ['item', 'achievement', 'action', 'gathering', 'instance', 'leve', 'enemy', 'emote', 'placename', 'status', 'title', 'recipe', 'quest', 'shop', 'npc', 'minion', 'mount', 'weather', 'fate'];

//'huntinglog', 'character',
var XIVDBTooltips = new XIVDBTooltipsClass(),
    XIVDBTooltipsDOM = new XIVDBTooltipsDOMClass(),
    XIVDBTooltipsLinks = new XIVDBTooltipsLinksClass(),
    XIVDBTooltipsHolder = new XIVDBTooltipsHolderClass(),
    XIVDBTooltipsDependency = new XIVDBTooltipsDependencyClass();

// start XIVDB Tooltips
document.addEventListener("DOMContentLoaded", function (event) {
    XIVDBTooltips.setOptions(typeof xivdb_tooltips !== 'undefined' ? xivdb_tooltips : xivdb_tooltips_default).init();
});