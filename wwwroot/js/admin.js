const lightTheme = "light-theme";
const activeTheme = "active";
const selectedTheme = localStorage.getItem("selected-theme");
const selectedIcon = localStorage.getItem("selected-icon");

const getCurrentTheme = () => document.body.classList.contains("dark-theme-variables") ? "dark" : "light";
const getCurrentActive = () => document.querySelector(".admin__right-top-theme-icon").classList.contains(activeTheme) ? "active" : "";

// Kiểm tra nếu có đặt chế độ trên Local Storage
if (selectedTheme) {
    document.body.classList[selectedTheme === "dark" ? "add" : "remove"]("dark-theme-variables");
}

if (selectedIcon == "active") {
    document.querySelector('.admin__right-top-theme-icon:nth-child(1)').classList.add("active");
    document.querySelector('.admin__right-top-theme-icon:nth-child(2)').classList.remove("active");
} else if (selectedIcon == null) {
    document.querySelector('.admin__right-top-theme-icon:nth-child(1)').classList.add("active");
    document.querySelector('.admin__right-top-theme-icon:nth-child(2)').classList.remove("active");
} else {
    document.querySelector('.admin__right-top-theme-icon:nth-child(1)').classList.remove("active");
    document.querySelector('.admin__right-top-theme-icon:nth-child(2)').classList.add("active");
}

function toggleTheme(div) {
    document.body.classList.toggle('dark-theme-variables');
    div.querySelector('i:nth-child(1)').classList.toggle('active');
    div.querySelector('i:nth-child(2)').classList.toggle('active');
    localStorage.setItem("selected-theme", getCurrentTheme());
    localStorage.setItem("selected-icon", getCurrentActive());
}



