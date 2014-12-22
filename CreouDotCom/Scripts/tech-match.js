var techMatch = {};

(function (exports) {

    function createTechObservable(data) {
        return {
            name: ko.observable(data.name),
            selected: ko.observable(data.selected),
            ex: ko.observable(data.ex),
            im: ko.observable(data.im),
            width: ko.observable((data.ex / 10 * 200) + 'px'),
            graphCategory: ko.computed(function () {
                if (data.ex > 6) {
                    return 'good';
                }
                if (data.ex > 3) {
                    return 'medium';
                }
                return 'bad';
            }),
            good: ko.observable(data.good),
            cm: ko.observable(data.cm),
            isCustom: ko.observable(data.isCustom),
            plc: ko.observable(data.plc ? data.plc : { message: '', good: true }),
            toggleSelect: function (item) {
                item.selected(!item.selected());
            }
        }
    }

    exports.init = function (container, techData, boundaryData) {

        var observableTechData = [];
        for (var i = 0; i < techData.length; i++) {
            observableTechData.push(createTechObservable(techData[i]));
        }

        var viewModel = {
            tech: ko.observableArray(observableTechData),
        }

        viewModel.selected = ko.computed(function () {
            var selectedItems = _.filter(this.tech(), function (item) {
                return item.selected();
            });

            return _.sortBy(selectedItems, function (item) {
                return 10 - item.ex();
            });
        }, viewModel);

        viewModel.contactUrl = ko.computed(function () {
            var techList = _.map(this.selected(), function (tech) {
                return tech.name();
            }).join('\n ');

            return 'contact?message=' + encodeURIComponent('Project details:\n ' + techList);
        }, viewModel);

        viewModel.selectedWithPlc = ko.computed(function () {
            var selectedPlc = _.filter(this.selected(), function (item) {
                return item.plc ? item.plc().message.length > 0 : false;
            });

            return _.sortBy(selectedPlc, function (item) {
                return !item.good();
            });
        }, viewModel);

        viewModel.overallCompatibility = ko.computed(function () {
            var sum = _.reduce(this.selected(), function (memo, value) {
                return memo + value.ex();
            }, 0);

            return Math.round((sum / viewModel.selected().length) / 10 * 100);
        }, viewModel);

        viewModel.overallCompatibilityBoundary = ko.computed(function () {

            var possibleBoundaries
              , highestBoundary;
            
            possibleBoundaries = _.filter(boundaryData, function (value) {
                return value.min <= viewModel.overallCompatibility();
            });

            highestBoundary = _.max(possibleBoundaries, function (value) {
                return value.min;
            });

            return highestBoundary;
        }, viewModel);

        $(container).find('#custom-tech button').click(function (evt) {
            evt.preventDefault();

            var customTechName = $(container).find('#custom-tech input').val();

            if (customTechName && customTechName.length > 0) {
                viewModel.tech.push(createTechObservable({ name: customTechName, ex: 0, isCustom: true, selected: true }));
                $(container).find('#custom-tech input').val('');
            }
        });

        ko.applyBindings(viewModel, container);   
    };
}(techMatch))