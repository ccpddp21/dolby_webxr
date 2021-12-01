mergeInto(LibraryManager.library, {

    Init: function(nameStr) {
        main(nameStr);
        return;
    },

    Create: function(nameStr) {
        createConference(nameStr);
        return;
    },

    Join: function(nameStr) {
        joinConference(nameStr);
        return;
    },

    Leave: function() {
        leaveConference();
        return;
    }

});