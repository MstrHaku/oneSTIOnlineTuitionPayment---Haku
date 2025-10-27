// DEBUGGER
console.log("This is gcash.js");

const phoneNumber = document.getElementById("phoneNumber");

// DISABLE INPUT NUMBER "e, +, and -"
phoneNumber.addEventListener('keydown', (e) => {
    if ((e.key >= '0' && e.key <= '9') || ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab'].includes(e.key)) {
        return null;
    }

    e.preventDefault();
});


// POP UP FLOW
// POP UP LAYOUTS
const loginPop = document.getElementById("loginPop");
const otpPop = document.getElementById("otpPop");
const mpinPop = document.getElementById("mpinPop");
const payDetPop = document.getElementById("payDetPop");
const paySucPop = document.getElementById("paySucPop");

// BUTTONS
const btn1 = document.getElementById("btn1");
const btn2 = document.getElementById("btn2");
const btn3 = document.getElementById("btn3");
const btn4 = document.getElementById("btn4");
const btn5 = document.getElementById("btn5");

btn1.addEventListener(`click`, () => {
    loginPop.classList.toggle("inactive");
    otpPop.classList.toggle("active");
    mpinPop.classList.remove("active");
    payDetPop.classList.remove("active");
    paySucPop.classList.remove("active");
});

btn2.addEventListener('click', () => {
    otpPop.classList.remove("active");
    mpinPop.classList.toggle("active");
    payDetPop.classList.remove("active");
    paySucPop.classList.remove("active");
});

btn3.addEventListener('click', () => {
    otpPop.classList.remove("active");
    mpinPop.classList.remove("active");
    payDetPop.classList.toggle("active");
    paySucPop.classList.remove("active");
});

btn5.addEventListener('click', () => {
    loginPop.classList.remove("inactive");
    otpPop.classList.remove("active");
    mpinPop.classList.remove("active");
    payDetPop.classList.remove("active");
    paySucPop.classList.remove("active");
});

// AVAILABLE BALANCE VALIDATION
const amountError = document.getElementById("amountError");
const payAmountForm = document.getElementById("payAmountForm");

payAmountForm.addEventListener("submit", async function (event) {
    event.preventDefault();
    amountError.textContent = "";

    const form = event.target;
    const totalAmount = parseInt(document.getElementById("totalAmount").value) || 0;

    try {
        const response = await fetch(form.action, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                Amount: totalAmount
            })
        });

        const data = await response.json();

        if (!response.ok) {
            console.warn("Seerver error: ", data);
            amountError.textContent = data?.message || "Amount exceeded!";
            btn4.innerHTML = "Go back to merchant";
            const url = btn4.dataset.url;
            btn4.addEventListener("click", () => {
                location.href = url;
            });
        } 
        else
        {
            otpPop.classList.remove("active");
            mpinPop.classList.remove("active");
            payDetPop.classList.remove("active");
            paySucPop.classList.toggle("active");
        }
    } 
    catch (err)
    {
        amountError.textContent = "Network error occured. Please try again later.";
    }
});
