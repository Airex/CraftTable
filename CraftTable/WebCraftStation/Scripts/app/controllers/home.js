
angular
    .module('craftStation', ['luegg.directives'])
    .controller('HomeController', function () {
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


        vm.act = function (ability) {
            console.log(ability.Name);
            try {
                vm.craftTable.act(CraftTable.CraftStation.getAbility(ability.Name));
            } catch (e) {
                console.log(e);
            }
            vm.history.push({ Name: ability.Name, XivDbId: ability.XivDbId });
            vm.status = vm.craftTable.getStatus();
            vm.setAbilityState();
            XIVDBTooltips.get();
        }

        vm.setAbilityState = function () {
            for (var i = 0; i < vm.model.Abilities.length; i++) {
                var m = vm.model.Abilities[i];
                var a = CraftTable.CraftStation.getAbility(m.Name);
                m.IsEnabled = vm.craftTable.canAct(a);
                m.IsHighLight = CraftTable.CraftStation.checkHighLight(a, vm.craftTable.getStatus());
            }
        }

        vm.reset = function () {
            vm.discard();
            delete vm.craftTable;
            delete vm.watcher;

            vm.history = [];
            vm.watcher = new CraftTable.Watcher();
            vm.craftTable = CraftTable.CraftStation.createCraftTable(vm.recipe, vm.craftMan, vm.watcher);
            vm.status = vm.craftTable.getStatus();
            vm.setAbilityState();
        }

        vm.settings = function () {
            vm.settingRecipe = jQuery.extend({}, vm.recipe);
            vm.settingCraftMan = jQuery.extend({}, vm.craftMan);
            angular.element("#settings").slideDown(500);
        }

        vm.discard = function () {
            angular.element("#settings").slideUp(500);
        }

        vm.save = function () {
            vm.recipe = jQuery.extend({}, vm.settingRecipe);
            vm.craftMan = jQuery.extend({}, vm.settingCraftMan);
            vm.reset();

            saveSettings();
            
            angular.element("#settings").slideUp(500);
        }

        vm.init = function (options) {
            loadSettings();
            if (!vm.recipe)
                vm.recipe = new CraftTable.Recipe(1968, 70, 13187, 190);
            if (!vm.craftMan)
                vm.craftMan = new CraftTable.CraftMan(CraftTable.Crafter.Culinarian, 995, 995, 437, 60);

            vm.model = {
                Abilities: CraftTable.CraftStation.getAbilitiesInfo(1).items,
                Categories: ['Synhtesis', 'Durability', 'Quality', 'CP', 'Other', 'Buffs', 'Specialist']
            };

            vm.reset();
            XIVDBTooltips.get();
        };

        function loadSettings() {
            if (window.localStorage) {
                var settings = window.localStorage.getItem("settings");
                if (settings) {
                    settings = JSON.parse(settings);
                    vm.recipe = new CraftTable.Recipe(settings.recipe.Difficulty, settings.recipe.Durability, settings.recipe.Quality, settings.recipe.Level);
                    vm.craftMan = new CraftTable.CraftMan(CraftTable.Crafter.Culinarian, settings.craftMan.Control, settings.craftMan.Craftmanship, settings.craftMan.CraftPoints, 60);
                }
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
                    }
                }));
            }
        }
    });
