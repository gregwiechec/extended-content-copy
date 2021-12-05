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
			alert(1);
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