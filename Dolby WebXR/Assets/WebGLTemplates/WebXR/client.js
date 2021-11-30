const avengersNames = [
    "Thor",
    "Cap",
    "Tony Stark",
    "Black Panther",
    "Black Widow",
    "Hulk",
    "Spider-Man",
];
let randomName =
    avengersNames[Math.floor(Math.random() * avengersNames.length)];

const CKEY = "dQQMU2k2NjhwDWDOQzOuXQ==";
const CSEC = "KHul95FKODp9CbV9J4smVVyNiIlyzpDROan-aJ9bo0w=";

const main = async () => {
    VoxeetSDK.initialize(CKEY, CSEC)
    try {
        // Open the session here !!!!
        await VoxeetSDK.session.open({ name: randomName });
        initUI();
    } catch (e) {
        alert('Something went wrong : ' + e);
    }
}

main();