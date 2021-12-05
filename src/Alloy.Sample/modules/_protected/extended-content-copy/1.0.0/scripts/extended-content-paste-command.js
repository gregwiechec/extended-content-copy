define([
    "dojo/_base/declare",
    "epi/shell/command/_Command",
	"epi-cms/command/PasteContent",
    "epi-cms/ApplicationSettings",
	"./extended-content-copy-dialog"
], function (
    declare,
    _Command,
	PasteContent,
	ApplicationSettings,
	extendedCopyDialog
) {
    return declare([PasteContent], {
        label: "Extended paste",

        _execute: function () {
        	var args = arguments;

			extendedCopyDialog().then(function(extraPasteHeaders) {
				this.model.service.extraPasteHeaders = extraPasteHeaders;

				var result = this.inherited(args);
				return result.then(function() {
					this.model.service.extraPasteHeaders = null;
				}.bind(this)).otherwise(function() {
					this.model.service.extraPasteHeaders = null;
				}.bind(this));
			}.bind(this))
			.otherwise(function() { });
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
