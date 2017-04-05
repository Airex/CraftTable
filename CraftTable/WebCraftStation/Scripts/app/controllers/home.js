
angular
    .module('craftStation', ['luegg.directives'])
        .controller('HomeController', function ($scope) {
            var vm = this;

            vm.formatCraftPoints = function (value) {
                return value > 0 ? value : '';
            }

            vm.formatCondition = function (condition) {
                switch (condition) {
                    case 0:
                        return "Normal";
                    case 1:
                        return "Good";
                    case 2:
                        return "Excellent";
                    case 3:
                        return "Poor";
                    default:
                }
                return '';
            }

            vm.formatBuff = function (value) {
                return value > 0 ? value : '';
            }

            vm.abilityMatch = function (category) {
                return function (item) {
                    var cc = crossClassForCrafter(vm.crafter);
                    return item.Category === category && (item.IsCrossClass === false || item.isForCrafter(vm.crafter) || cc.indexOf(item.Name) >= 0);
                };
            }

            vm.act = function (ability) {
                if (!ability.IsEnabled) return;
                var condition = vm.status.Condition;
                var failed = false;
                console.log(ability.Name);
                try {
                    var overrides = CraftTable.CraftStation.createOverride(condition, null);
                    vm.craftTable.act(CraftTable.CraftStation.getAbility(ability.Name), overrides);
                } catch (e) {
                    console.log(e);
                    if (e.$$fullname === 'CraftTable.Exceptions.AbilityFailedException')
                        failed = true;
                }

                vm.status = vm.craftTable.getStatus();
                vm.history.push({ Name: ability.Name, XivDbId: ability.XivDbId, Condition: condition, Failed: failed });
                vm.setAbilityState();
                XIVDBTooltips.get();
            }

            vm.isUsedInCrossClass = function (item) {
                var cc = crossClassForCrafter(vm.crafter);
                return cc.indexOf(item.Name) >= 0;
            }

            vm.setAbilityState = function () {
                for (var i = 0; i < vm.model.Abilities.length; i++) {
                    var m = vm.model.Abilities[i];
                    var a = CraftTable.CraftStation.getAbility(m.Name);
                    var condition = vm.status.Condition;
                    var overrides = CraftTable.CraftStation.createOverride(condition, null);
                    m.IsEnabled = vm.craftTable.canAct(a, overrides);
                    m.IsHighLight = CraftTable.CraftStation.checkHighLight(a, vm.status);
                }
            }

            function crossClassForCrafter(crafter) {
                vm.crossCrossClass = vm.crossCrossClass || {};
                if (!vm.crossCrossClass.hasOwnProperty(crafter))
                    vm.crossCrossClass[crafter] = [];
                return vm.crossCrossClass[crafter];
            }

            vm.reset = function () {
                vm.discard();
                vm.discardSearch();
                delete vm.craftTable;
                delete vm.watcher;

                vm.history = [];
                vm.watcher = new CraftTable.Watcher();
                vm.craftTable = CraftTable.CraftStation.createCraftTable(vm.recipe, vm.craftMan, vm.watcher);
                vm.status = vm.craftTable.getStatus();
                vm.setAbilityState();
            }

            vm.searchRecipe = function () {
                angular.element('#search').slideDown();
                angular.element("#stats").hide();
                angular.element("#settings").hide();
            }

            vm.settings = function () {
                vm.settingRecipe = jQuery.extend({}, vm.recipe);
                vm.settingCraftMan = jQuery.extend({}, vm.craftMan);
                angular.element("#settings").slideDown(500);
                angular.element("#stats").hide();
                angular.element('#search').hide();
            }

            vm.categories = function (dummy) {
                return vm.model.Categories;
            }

            vm.discard = function () {
                angular.element("#settings").slideUp(500);
                angular.element("#stats").show();
            }

            vm.discardSearch = function () {
                angular.element('#search').slideUp();
                angular.element("#stats").show();
            }

            vm.onCrossClass = function (item) {
                var cc = crossClassForCrafter(vm.crafter);


                if (cc.indexOf(item.Name) >= 0)
                    cc.splice(cc.indexOf(item.Name), 1);
                else {
                    if (cc.length === 10) return;
                    cc.push(item.Name);
                }

                vm.counter++;
            }

            vm.selectRecipe = function (item) {
                vm.recipe = new CraftTable.Recipe(item.difficulty, item.durability, item.quality, item.level);
                vm.reset();
                angular.element('#search').slideUp();
            }

            vm.abilityCrossClassMatch = function (item) {
                return item.IsCrossClass === true && (!item.isForCrafter(vm.crafter));
            }

            vm.getRecipeImageId = function (item) {
                vm.imagesCache = vm.imagesCache || {};
                var at = angular.element("#recipe_" + item.id).attr('src');
                if (vm.imagesCache["recipe_" + item.id] && !at) {
                    window.setTimeout(function () {
                        angular.element("#recipe_" + item.id).attr('src', vm.imagesCache["recipe_" + item.id]);
                    }, 0);

                } else {
                    $.get("https://api.xivdb.com/recipe/" + item.id,
                        '',
                        function (id) {
                            return function (data) {
                                if (data) {
                                    var icon = data["icon"];
                                    angular.element("#recipe_" + id).attr('src', icon);
                                    vm.imagesCache["recipe_" + id] = icon;
                                }
                            }
                        }(item.id));
                }
                return "recipe_" + item.id;
            }

            vm.crossClassUsedCount = function () {
                return crossClassForCrafter(vm.crafter).length;
            }

            vm.recipeSearchMatch = function (item) {
                if (!vm.recipeSearch) return false;
                if (!vm.recipeSearch.name) return false;
                if (vm.recipeSearch.name.length < 3) return false;
                return item.name.toLowerCase().indexOf(vm.recipeSearch.name.toLowerCase()) >= 0 && convertToCrafter(item.classjob) === vm.crafter;
            };

            function convertToCrafter(job) {
                switch (job + "") {
                    case "9": return "64";
                    case "10": return "32";
                    case "12": return "16";
                    case "13": return "8";
                    case "11": return "4";
                    case "15": return "1";
                    case "8": return "128";
                    case "14": return "2";
                }
            }

            vm.save = function () {
                vm.recipe = jQuery.extend({}, vm.settingRecipe);
                vm.craftMan = jQuery.extend({}, vm.settingCraftMan);
                vm.reset();

                saveSettings();

                angular.element("#settings").slideUp(500);
                angular.element("#stats").show();
            }

            vm.crafterChanged = function () {
                vm.model = {
                    Abilities: CraftTable.CraftStation.getAbilitiesInfo(Number(vm.crafter)).items,
                    Categories: ['Synhtesis', 'Durability', 'Quality', 'CP', 'Other', 'Buffs', 'Specialist']
                };
                vm.setAbilityState();
                saveCrafter();
            }

            vm.init = function () {
                loadSettings();
                loadCrafter();

                if (!vm.crafter)
                    vm.crafter = CraftTable.Crafter.Culinarian + "";
                if (!vm.recipe)
                    vm.recipe = new CraftTable.Recipe(1968, 70, 13187, 190);
                if (!vm.craftMan)
                    vm.craftMan = new CraftTable.CraftMan(vm.crafter, 995, 995, 437, 60);

                vm.counter = 0;

                vm.model = {
                    Abilities: CraftTable.CraftStation.getAbilitiesInfo(Number(vm.crafter)).items,
                    Categories: ['Synhtesis', 'Durability', 'Quality', 'CP', 'Other', 'Buffs', 'Specialist']
                };

                $.get('https://api.xivdb.com/recipe?columns=id,name,level,item,difficulty,durability,quality,classjob', null, function (data) {
                    vm.recipes = data;
                });

                vm.reset();
                XIVDBTooltips.get();
            }

            vm.undo = function () {
                var savedCondition = vm.history[vm.history.length - 1].Condition;
                vm.history.splice(vm.history.length - 1, 1);


                vm.watcher = new CraftTable.Watcher();
                vm.craftTable = CraftTable.CraftStation.createCraftTable(vm.recipe, vm.craftMan, vm.watcher);

                for (var i = 0; i < vm.history.length; i++) {
                    var ab = vm.history[i];
                    console.log(ab.Name);
                    try {
                        vm.craftTable.act(CraftTable.CraftStation.getAbility(ab.Name), CraftTable.CraftStation.createOverride(ab.Condition, ab.Failed));
                    } catch (e) {
                        console.log(e);
                    }
                }
                vm.status = vm.craftTable.getStatus();
                vm.status.Condition = savedCondition;
                vm.setAbilityState();
                XIVDBTooltips.get();
            }

            vm.crafters = function () {
                return {
                    1: 'Culinarian',
                    2: 'Alchemist',
                    4: 'Goldsmith',
                    8: 'Weaver',
                    16: 'Leatherworker',
                    32: 'Armorer',
                    64: 'Blacksmith',
                    128: 'Carpenter'
                };
            }

            function loadSettings() {
                if (window.localStorage) {
                    var settings = window.localStorage.getItem("settings");
                    if (settings) {
                        settings = JSON.parse(settings);
                        vm.recipe = new CraftTable.Recipe(settings.recipe.Difficulty, settings.recipe.Durability, settings.recipe.Quality, settings.recipe.Level);
                        vm.craftMan = new CraftTable.CraftMan(vm.crafter, settings.craftMan.Control, settings.craftMan.Craftmanship, settings.craftMan.CraftPoints, 60);
                        vm.crossCrossClass = settings.crossClass;
                    }
                }
            }

            function loadCrafter() {
                if (window.localStorage) {
                    var settings = window.localStorage.getItem("settings-crafter");
                    if (settings) {
                        settings = JSON.parse(settings);
                        vm.crafter = settings.crafter || CraftTable.Crafter.Culinarian + "";
                    }
                }
            }

            function saveCrafter() {
                if (window.localStorage) {
                    window.localStorage.setItem("settings-crafter", JSON.stringify(
                    {
                        crafter: vm.crafter
                    }));
                }
            }

            function saveSettings() {
                if (window.localStorage) {
                    window.localStorage.setItem("settings", JSON.stringify(
                    {
                        recipe: {
                            Difficulty: vm.recipe.Difficulty,
                            Durability: vm.recipe.Durability,
                            Quality: vm.recipe.MaxQuality,
                            Level: vm.recipe.Level
                        },
                        craftMan: {
                            Control: vm.craftMan.Control,
                            Craftmanship: vm.craftMan.Craftmanship,
                            CraftPoints: vm.craftMan.MaxCraftPoints
                        },
                        crossClass: vm.crossCrossClass
                    }));
                }
            }
        });
