mergeInto(LibraryManager.library, {

    Create: function(nameStr) {
        createConference(nameStr);
    },

    Join: function(nameStr) {
        joinConference(nameStr);
    },

    Leave: function() {
        leaveConference();
    }

});