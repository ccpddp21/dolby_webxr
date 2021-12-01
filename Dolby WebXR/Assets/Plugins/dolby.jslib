mergeInto(LibraryManager.library, {

    Init: function(nameStr) {
        main(Pointer_stringify(nameStr));
        return;
    },

    Create: function(nameStr) {
        createConference(Pointer_stringify(nameStr));
        return;
    },

    Join: function(nameStr) {
        joinConference(Pointer_stringify(nameStr));
        return;
    },

    Leave: function() {
        leaveConference();
        return;
    }

});