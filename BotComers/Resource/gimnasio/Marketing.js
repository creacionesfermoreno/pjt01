
const durationTime = 50;

function HideBackdrop() {
    let timer;
    window.clearTimeout(timer);
    timer = window.setTimeout(() => {
        if (document.querySelector(".modal-backdrop")) {
            document.querySelector(".modal-backdrop").classList.remove("modal-backdrop");
        }
    }, durationTime);
}


/****************************** menus emails ****************************/
document.getElementById("DSendEmailMasive").style.display = "none";
document.getElementById("DCampaingEmail").style.display = "block";
function setEmail() {
    console.log("sfd")
    let item = document.getElementById("CampaingEmailItem");
    evemt_changeMenu(item, 1);
}
function evemt_changeMenu(e, num) {
    document.querySelectorAll(".DItemsEmails").forEach((item, index) => {
        item.style.display = 'none';
    });

    document.querySelectorAll(".item_menu_sd").forEach((item, index) => {
        item.classList.remove("active")
    });

    switch (num) {
        case 1:
            document.getElementById("DCampaingEmail").style.display = "block";
            e.classList.add("active");
            break;
        case 2:
            document.getElementById("DSendEmailMasive").style.display = 'block';
            e.classList.add("active");
            break;
        case 3:
            document.getElementById("Dconfiguration").style.display = "block";
            e.classList.add("active");
            break;
        default:
            break;
    }
}
/****************************** menus emails ****************************/