define([
	"epi-cms/component/ContentContextMenuCommandProvider"
], function (
	ContentContextMenuCommandProvider
) {
    return function () {
		
		//
		// commands added through navigationTreePluginArea doesn't have access to clipboard, selection and model
		// We need them, to make extended paste command work similar to default paste command
		//
		
        var originalUpdateCommands = ContentContextMenuCommandProvider.prototype._updateCommands;
        ContentContextMenuCommandProvider.prototype._updateCommands = function () {
            originalUpdateCommands.apply(this, arguments);
			
			this.commands.forEach(function(command) {
				if (command.label === "Extended paste") {
					command.clipboard = this._settings.clipboard;
					command.selection = this._settings.selection;
					command.model = this._settings.model;
					command.watchClipboardChange();
					command.watchSelectionChange();
				}
			}, this);
        };
        ContentContextMenuCommandProvider.prototype._updateCommands.nom = "_updateCommands";
    };
});


