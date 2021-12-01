// ui.js
const initUI = () => {
    //alert("Init");
};

const createConference = (conferenceAlias) => {
    /*
    1. Create a conference room with an alias
    2. Join the conference with its id
    */
    VoxeetSDK.conference.create({ alias: conferenceAlias })
        .then((conference) => VoxeetSDK.conference.join(conference, {}))
        .then(() => {
            alert("Created Conference");
        })
        .catch((err) => console.error(err));
}

const joinConference = (conferenceAlias) => {
    VoxeetSDK.conference.fetch({ alias: conferenceAlias })
        .then((conference) => VoxeetSDK.conference.join(conference, {}))
        .then(() => {
            alert("Joined Conference");
        })
        .catch((err) => console.error(err));
};

const leaveConference = () => {
    VoxeetSDK.conference
        .leave()
        .then(() => {
            alert("Left Conference");
        })
        .catch((err) => console.error(err));
};