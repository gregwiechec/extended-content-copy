define([
	"epi-cms/contentediting/ContentHierarchyService"
], function (
	ContentHierarchyService
) {
    return function () {
		
		//
		// we need to send header parameters with informations about
		// how page should be copied
		//
		
        var originalCopy = ContentHierarchyService.prototype.copy;
        ContentHierarchyService.prototype.copy = function (sourceIds, targetId, createAsLocalAsset, sortIndex) {
            var headers = undefined;
			if (this.extraPasteHeaders) {
				headers = Object.assign({}, this.extraPasteHeaders);
				this.extraPasteHeaders = null;
			}
			
			// the method is copied from original "_execute" method, but headers parameter was added
			var params = {
                ids: sourceIds instanceof Array ? sourceIds : [sourceIds],
                targetId: targetId + "",
                createAsLocalAsset: createAsLocalAsset,
                sortIndex: sortIndex
            };

            return this.store.executeMethod("CopyMany", null, params, headers)
                .then(this._checkForErrors.bind(this))
                .then(this._handleUpdates.bind(this));
        };
        ContentHierarchyService.prototype.copy.nom = "copy";
    };
});


