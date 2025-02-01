mergeInto(LibraryManager.library, {
    SyncIndexedDB: function(onComplete) {
        console.log("[SyncIndexedDB] Syncing IndexedDB...");

        if (typeof FS !== "undefined" && FS.syncfs) {
            FS.syncfs(false, function(err) {
                if (err) {
                    console.error("[SyncIndexedDB] Sync error:", err);
                } else {
                    console.log("[SyncIndexedDB] Sync complete.");
                    if (onComplete) {
                        dynCall("v", onComplete);
                    }
                }
            });
        } else {
            console.warn("[SyncIndexedDB] FS.syncfs not found.");
            if (onComplete) {
                dynCall("v", onComplete);
            }
        }
    }
});
