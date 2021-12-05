define([
    "dojo/_base/declare",
    "dojo/Deferred",
    "dojo/on",
	"epi/shell/widget/dialog/Dialog"    
], function (
    declare,
	on,
	Deferred,
	Dialog    
) {
	var template = `<div>
		<input id="extendedPastePublishOnCopy" name="extendedPastePublishOnCopy" data-dojo-attach-point="publishOnCopy" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPastePublishOnCopy">Publish on copy</label>
		<input id="extendedPasteLanguages" name="extendedPasteLanguages" data-dojo-attach-point="languages" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPasteLanguages">Publish all languages</label>
		<input id="extendedPasteDescendants" name="extendedPasteDescendants" data-dojo-attach-point="descendants" data-dojo-type="dijit/form/CheckBox" checked /> <label for="extendedPasteDescendants">Copy descendants</label>
	</div>`;
	
    var DialogContent = declare([_TemplatedMixin], {
        template: template,
		
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
	
	return function() showExtendedPaste() {
		var deferred = new Deferred();
	
		var dialog = new Dialog({
           dialogClass: "search-content-dialog",
           defaultActionsVisible: false,
           //confirmActionText: sharedResources.action.save,
           content: searchBox,
           //title: editsecurityResources.title
        });
		
		var dialogContent = new DialogContent();
		
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