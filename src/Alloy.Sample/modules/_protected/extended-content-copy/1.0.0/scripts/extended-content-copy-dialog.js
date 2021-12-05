define([
    "dojo/_base/declare",
    "dojo/Deferred",
    "dojo/on",

    "dijit/_Widget",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

	"epi/shell/widget/dialog/Dialog",
    "epi-cms/ApplicationSettings",
], function (
    declare,
    Deferred,
	on,

    _Widget,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

	Dialog,
    ApplicationSettings
) {
	var template = `<div style="width: 300px;">
		<div data-dojo-attach-point="publishAvailable">
			<input id="extendedPastePublishOnCopy" name="extendedPastePublishOnCopy" data-dojo-attach-point="publishOnCopy" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPastePublishOnCopy">Publish on copy</label>
		</div>
		<div data-dojo-attach-point="languagesAvailable">
			<input id="extendedPasteLanguages" name="extendedPasteLanguages" data-dojo-attach-point="allLanguages" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPasteLanguages">Publish all languages</label>
		</div>
		<div data-dojo-attach-point="descendantsAvailable">
			<input id="extendedPasteDescendants" name="extendedPasteDescendants" data-dojo-attach-point="descendants" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPasteDescendants">Copy descendants</label>
		</div>
	</div>`;

    var DialogContent = declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], {
        templateString: template,

        postCreate: function () {
            // call base implementation
            this.inherited(arguments);

            var defaults = ApplicationSettings.extendedContentPasteDefaults || {};
            this.publishOnCopy.checked = defaults.publishOnDestination;
            this.allLanguages = defaults.copyAllLanguageBranches;
            this.descendants = defaults.copyDescendants;

            var available = ApplicationSettings.extendedContentCopy || {};
            this.publishAvailable.classList.toggle("dijitHidden", !available.publishOnDestination);
            this.languagesAvailable.classList.toggle("dijitHidden", !available.copyAllLanguageBranches);
            this.descendantsAvailable.classList.toggle("dijitHidden", !available.copyDescendants);
        },

		_getCheckboxValue: function(checkbox) {
			return checkbox.checked ? "true": "false";
		},

		_getValueAttr: function() {
			return {
				extendedPaste: "true",
				extendedPastePublish: this._getCheckboxValue(this.publishOnCopy),
				extendedPasteLanguages: this._getCheckboxValue(this.allLanguages),
				extendedPasteDescendants: this._getCheckboxValue(this.descendants),
			};
		}
    });

	return function showExtendedPaste() {
		var deferred = new Deferred();

        var dialogContent = new DialogContent();

		var dialog = new Dialog({
           dialogClass: "search-content-dialog",
           defaultActionsVisible: true,
           confirmActionText: "Paste",
           content: dialogContent,
           title: "Paste content"
        });

        dialog.own(dialogContent);

		on.once(dialog, "execute", function (value) {
			deferred.resolve(dialogContent.get("value"));
		});
        on.once(dialog, "cancel", function () {
            deferred.reject();
        });

        dialog.show();

		return deferred.promise;
	}
});
