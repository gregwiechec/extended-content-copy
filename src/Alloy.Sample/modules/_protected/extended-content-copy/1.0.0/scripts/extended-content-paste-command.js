define([
    "dojo/_base/declare",
    "epi/shell/command/_Command",
	"epi-cms/command/PasteContent",
    "epi-cms/ApplicationSettings"
], function (
    declare,
    _Command,
	PasteContent,
	ApplicationSettings
) {
    return declare([PasteContent], {
        label: "Extended paste",

        _execute: function () {
			this.model.service.extraPasteHeaders = {
				extendedPaste: "true",
				extendedPastePublish: "false",
				extendedPasteLanguages: "false",
				extendedPasteDescendants: "false"
			};
			var result = this.inherited(arguments);
			return result.then(function() {
				this.model.service.extraPasteHeaders = null;
			}.bind(this)).otherwise(function() {
				this.model.service.extraPasteHeaders = null;
			}.bind(this));
        },
		
		 _onModelChange: function () {
			 if (this.clipboard.copy) {
				this.inherited(arguments);
			 } else {
				// command is not available for move command
				this.set("canExecute", false);
			 }
		 },

		watchClipboardChange: function() {
			if (!this.clipboard) {
				return;
			}
			this.inherited(arguments);
		},
		
		watchSelectionChange: function() {
			if (!this.selection) {
				return;
			}
			this.inherited(arguments);
		}
    });
});