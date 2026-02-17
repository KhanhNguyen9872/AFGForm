<div align="center">
  <h1>AFGForm</h1>
  <p><strong>Auto Filler Google Form</strong> <br /> A powerful tool to automate Google Form submissions with random or custom data.</p>
</div>

<div align="center">

[![Status](https://img.shields.io/badge/status-active-success.svg)]()
[![Platform](https://img.shields.io/badge/platform-Windows-blue.svg)]()
[![GitHub Issues](https://img.shields.io/github/issues/KhanhNguyen9872/AFGForm.svg)](https://github.com/KhanhNguyen9872/AFGForm/issues)
[![GitHub Pull Requests](https://img.shields.io/github/issues-pr/KhanhNguyen9872/AFGForm.svg)](https://github.com/KhanhNguyen9872/AFGForm/pulls)

</div>

## 🌟 Introduction

**AFGForm** (Auto Filler Google Form) is a Windows-based application designed to automate the process of filling and submitting Google Forms. Whether you need to test a form, generate sample data, or perform bulk submissions, AFGForm provides a user-friendly interface to manage and execute these tasks efficiently.

It supports a wide range of question types including grids, linear scales, and date/time fields, allowing for realistic and varied data generation.

## 📱 Screenshots

<div align="center">
    <img src="https://github.com/KhanhNguyen9872/AFGForm/raw/main/img/img000.png" alt="AFGForm Screenshot" width="800" />
</div>

## ✨ Key Features

-   **🔄 Automated Submissions**: Perform multiple form submissions automatically with specified repeat counts.
-   **🎲 Random Data Generation**: Automatically generate random answers for various field types.
-   **📝 Support for Complex Fields**:
    -   Short Answer & Paragraphs
    -   Multiple Choice & Checkboxes
    -   Dropdowns
    -   Linear Scale
    -   **Multi-choice & Checkbox Grids**
    -   Date & Time
-   **⚡ High Performance**: Multi-threaded process for efficient execution.
-   **🎛️ Detailed Control**:
    -   Configurable delay between submissions.
    -   "Random as radio" mode for checkbox grids.
    -   Option to ignore "Other" fields during randomization.
-   **🌐 Smart Form Parsing**: Automatically fetches and parses questions from a Google Form URL.

## 🛠️ Tech Stack

-   **[C# .NET](https://dotnet.microsoft.com/)**: Core application logic.
-   **[Windows Forms (WinForms)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)**: User Interface.
-   **[HttpClient](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient)**: For web requests and form submission.

## 🚀 Getting Started

### Prerequisites

-   Windows OS (Windows 10/11 recommended).
-   **.NET Framework** (Ensure you have the runtime installed).

### Installation

1.  **Download the latest release**:
    Visit the [Releases](https://github.com/KhanhNguyen9872/AFGForm/releases) page and download the executable file.

2.  **Run the application**:
    Extract the zip file (if applicable) and run `AFGForm.exe`.

### Build from Source

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/KhanhNguyen9872/AFGForm.git
    ```
2.  **Open in Visual Studio**:
    Open the `AFGForm.sln` solution file in Visual Studio 2022 or later.
3.  **Restore Nuget Packages**:
    Ensure all dependencies are restored.
4.  **Build**:
    Build the solution in `Release` mode.

## 📖 Usage

1.  **Enter Form URL**: Paste your Google Form URL (must contain `viewform`) into the URL text box.
2.  **Get Data**: Click **Get Data** to fetch the questions.
3.  **Configure Answers**:
    -   Click on a question row to configure specific answers or enabling randomization.
    -   Check **Random** to let the tool pick answers automatically.
4.  **Settings**:
    -   **Repeat**: Number of times to submit the form.
    -   **Delay**: Configure interval between submissions.
5.  **Start**: Click **Start** to begin the automation.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

## ✍️ Author

**Nguyễn Văn Khánh** (KhanhNguyen9872)

-   GitHub: [@KhanhNguyen9872](https://github.com/KhanhNguyen9872)

---

<p align="center">Made with ❤️</p>
