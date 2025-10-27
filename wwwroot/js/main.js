// DEBUGGING FUNCTIONS
function debug(context, message) {
    console.log(context + ": " + message);
}

// RESPONSIVE
function handleSidebar(screenSize) {
    if (screenSize.matches) {
        siteSidebar.classList.remove("toggle");
        main.classList.remove("toggle");
        debug("Sidebar", "Remove Sidebar");
    } 
}

// DEBUGGER
console.log("This is main.js");

// SIDEBAR TOGGLE
const hamBtnDesk = document.getElementById("hamBtnDesk");
const hamBtnPhone = document.getElementById("hamBtnPhone");
const siteSidebar = document.getElementById("siteSidebar");
const main = document.getElementById("main");
const navItems = document.getElementById("navItems");

    hamBtnDesk.addEventListener("click", () => {
        siteSidebar.classList.toggle("toggle");
        main.classList.toggle("toggle");
        navItems.classList.toggle("toggle");
    });

    hamBtnPhone.addEventListener("click", () => {
        siteSidebar.classList.toggle("toggle");
        main.classList.toggle("toggle");
        navItems.classList.toggle("toggle");
    });

// TABLET 
const tablet = window.matchMedia("(max-width: 1120px)");
handleSidebar(tablet);
tablet.addEventListener('change', handleSidebar);

// PHONE
const phone = window.matchMedia("(max-width: 767px)");
handleSidebar(phone);
phone.addEventListener('change', handleSidebar);



// PAYMENT TRANSACTION ---------------------------------------------

// POP UP LAYOUT
const payAmountForm = document.getElementById("payAmountForm");
const paymentMethodCon = document.getElementById("paymentMethodCon");
const transactionCon = document.getElementById("transactionCon");
const transactionDetailsCon = document.getElementById("transactionDetailsCon");

// PAYMENT METHOD
const gcashPayment = document.getElementById("gcashPayment");
const mayaPayment = document.getElementById("mayaPayment");

gcashPayment.addEventListener("click", () => {
    paymentMethodCon.classList.toggle("inactive");
    transactionCon.classList.toggle("active");
    transactionDetailsCon.classList.remove("active");

    // PAYMENT METHOD TEXT
    document.getElementById("paymentMethodPTag").textContent = "GCash";
});

mayaPayment.addEventListener("click", () => {
    paymentMethodCon.classList.toggle("inactive");
    transactionCon.classList.toggle("active");
    transactionDetailsCon.classList.remove("active");

    // PAYMENT METHOD TEXT
    document.getElementById("paymentMethodPTag").textContent = "PayMaya";
});

// PAYMENT AMOUNT POP UP 
const amount = document.getElementById("amount");

// DISABLE INPUT NUMBER "e, +, and -"
amount.addEventListener('keydown', (e) => {
    if ((e.key >= '0' && e.key <= '9') || ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab'].includes(e.key)) {
        return null;
    }

    e.preventDefault();
});

// PAYMENT METHOD VALUE SENT TO ASP.NET (CONTROLLER)
const paymentMethod = document.getElementById("paymentMethod");
const paymentMethodPTag = document.getElementById("paymentMethodPTag");

debug("Payment Method", paymentMethodPTag.textContent);

document.querySelector("form").addEventListener("submit", function() {
    paymentMethod.value = paymentMethodPTag.textContent;
})

// AMOUNT FORM VALIDATION
payAmountForm.addEventListener("submit", function(e) {
    const amount = document.getElementById("amount");
    if (!amount) {
        e.preventDefault();
        alert("Enter amount");
    }
});

// CLOSE BTN AND PAY NOW BTN
const payNowBtn = document.getElementById("payNowBtn");
const closeBtn = document.getElementById("closeBtn");
const popUp = document.getElementById("popUp");

    payNowBtn.addEventListener("click", () => {
        popUp.classList.toggle("active")
        paymentMethodCon.classList.remove("inactive");
    });

    closeBtn.addEventListener("click", () => {
        popUp.classList.remove("active");
        transactionCon.classList.remove("active");
        transactionDetailsCon.classList.remove("active");
    });
    

// REMAINING BALANCE VALIDATION
const amountError = document.getElementById("amountError");
payAmountForm.addEventListener("submit", async function (event) {
    event.preventDefault(); 
    amountError.textContent = "";

    const form = event.target;
    const amount = parseFloat(document.getElementById("amount").value);
    const paymentMethod = document.getElementById("paymentMethod").value;

    try {
        const response = await fetch(form.action, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ 
                Amount: amount,
                PaymentMethod: paymentMethod
            })
        });

        const data = await response.json();

        if (!response.ok) {
            amountError.textContent = data?.message || "Amount exceeded!";
            console.warn("Server error: ", data);
        } else if (data.redirectUrl) {
            window.location.href = data.redirectUrl;
        }
    } catch (err) {
        amountError.textContent = "Network error occured. Please try again later.";
    }
});



