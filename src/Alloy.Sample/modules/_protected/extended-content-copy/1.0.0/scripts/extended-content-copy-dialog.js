define([
    "dojo/_base/declare",
    "dojo/Deferred",
    "dojo/on",

    "dijit/_Widget",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

	"epi/shell/widget/dialog/Dialog"
], function (
    declare,
    Deferred,
	on,

    _Widget,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

	Dialog
) {
	var template = `<div style="width: 300px;">
		<div>
			<input id="extendedPastePublishOnCopy" name="extendedPastePublishOnCopy" data-dojo-attach-point="publishOnCopy" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPastePublishOnCopy">Publish on copy</label>
		</div>
		<div>
			<input id="extendedPasteLanguages" name="extendedPasteLanguages" data-dojo-attach-point="languages" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPasteLanguages">Publish all languages</label>
		</div>
		<div>
			<input id="extendedPasteDescendants" name="extendedPasteDescendants" data-dojo-attach-point="descendants" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPasteDescendants">Copy descendants</label>
		</div>
	</div>`;

    var DialogContent = declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], {
        templateString: template,

		_getCheckboxValue: function(checkbox) {
			return checkbox.checked ? "true": "false";
		},

		_getValueAttr: function() {
			return {
				extendedPaste: "true",
				extendedPastePublish: this._getCheckboxValue(this.publishOnCopy),
				extendedPasteLanguages: this._getCheckboxValue(this.languages),
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
