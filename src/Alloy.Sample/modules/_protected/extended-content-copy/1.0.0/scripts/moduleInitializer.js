define([
    "dojo/_base/declare",

    "epi/_Module",
    "epi-cms/ApplicationSettings",
	"./ContentContextMenuCommandProvider-initialization",
	"./ContentHierarchyService-initialization",
	"epi-cms/plugin-area/navigation-tree",
    "./extended-content-paste-command"
], function (
    declare,

    _Module,
    ApplicationSettings,
	contentContextMenuCommandProviderInitialization,
	contentHierarchyServiceInitialization,
	navigationTreePluginArea,
	ExtendedContentPaste
) {
    return declare([_Module], {
        initialize: function () {
            this.inherited(arguments);

            //ApplicationSettings.configurationContainerLinks = this._settings.configurationContainerLinks || [];

            //if (this._settings.command.enabled) {
				contentHierarchyServiceInitialization();
				contentContextMenuCommandProviderInitialization();
				navigationTreePluginArea.add(ExtendedContentPaste);
            //}
        }
    });
});