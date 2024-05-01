import serial.tools.list_ports
import subprocess
import os
import requests
import zipfile

# Function to get the latest workflow run ID
def get_workflow_run_id(owner, repo, token):
    choice = input("Do you want to download artifacts from the latest workflow run? (Y/N): ").strip().lower()
    if choice == "y":
        headers = {"Authorization": f"token {token}"}
        url = f"https://api.github.com/repos/{owner}/{repo}/actions/runs"
        response = requests.get(url, headers=headers)

        if response.status_code == 200:
            workflow_runs = response.json()["workflow_runs"]
            if workflow_runs:
                latest_run = workflow_runs[0]  # First item is the latest run
                return latest_run["id"]
            else:
                print("No workflow runs found.")
                return None
        else:
            print(f"Failed to fetch workflow runs. Status code: {response.status_code}")
            return None
    elif choice == "n":
        return input("Enter the workflow ID to download artifacts from: ").strip()
    else:
        print("Invalid choice. Please enter Y or N.")
        return None


# Function to download GitHub workflow artifacts
def download_github_artifact(owner, repo, run_id, token, artifact_name):
    headers = {"Authorization": f"token {token}"}
    url = f"https://api.github.com/repos/{owner}/{repo}/actions/runs/{run_id}/artifacts"
    response = requests.get(url, headers=headers)

    if response.status_code == 200:
        artifacts = response.json()["artifacts"]
        for artifact in artifacts:
            if artifact["name"] == artifact_name:
                download_url = artifact["archive_download_url"]
                print(f"Downloading artifact '{artifact_name}'...")
                response = requests.get(download_url, headers=headers)
                if response.status_code == 200:
                    zip_file_path = f"{artifact_name}.zip"
                    with open(zip_file_path, "wb") as file:
                        file.write(response.content)

                    with zipfile.ZipFile(zip_file_path, "r") as zip_ref:
                        zip_ref.extractall()

                    print(f"Artifact '{artifact_name}' downloaded and unzipped successfully.")
                    os.remove(zip_file_path)
                    return True
                else:
                    print(f"Failed to download artifact '{artifact_name}'. Status code: {response.status_code}")
                    return False
        print(f"Artifact '{artifact_name}' not found.")
        return False
    else:
        print(f"Failed to fetch artifacts. Status code: {response.status_code}")
        return False




def find_connected_devices():
    return [
        (p.device, p.description.split(':')[-1].strip())
        for p in serial.tools.list_ports.comports()
        if 'USB Serial Device' in p.description or 'Arduino Mega 2560' in p.description
    ]


# Flash the firmware using AVRDUDE for Arduino devices
def flash_firmware():
    for port, description in connected_devices:
        print(f"\nFlashing firmware to {port} at {BAUD_RATE} baud...")
        command = [
            "avrdude.exe",
            "-v", "-p", "atmega2560", "-c", "wiring",
            "-P", port, "-b", BAUD_RATE,
            "-D", "-U", "flash:w:firmware.hex:i"
        ]
        print(command)
        try:
            subprocess.run(command, check=True)
        except subprocess.CalledProcessError as e:
            print("Error:", e)

def run_tests():
    download_github_artifact(owner, repo, run_id, token, "test-env")
    command = [
        "platformio",
        "test",
        "--environment", "target_run"
    ]
    print("Running tests...")
    try:
        result = subprocess.run(command, stdout=subprocess.PIPE, stderr=subprocess.STDOUT, text=True)
        # Print test output
        print(result.stdout)
        # Check test result
        if "FAILED" in result.stdout:
            print("Tests failed. Firmware will not be flashed")
        else:
            print("Tests passed. Flashing to avr...")
            flash_firmware()
    except subprocess.CalledProcessError as e:
        print("Error:", e)

# Provide GitHub repository information to retrieve workflow ID
owner = "JonasPHenriksen"
repo = "IoTCICDTest"
token = "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"

# Set the baud rate (115200 for Arduino Mega 2560)
BAUD_RATE = "115200"

# Get the latest workflow run ID or costume input
run_id = get_workflow_run_id(owner, repo, token)

if run_id:
    bypass = download_github_artifact(owner, repo, run_id, token, "firmware")

# Get a list of all connected devices
connected_devices = find_connected_devices()

if not connected_devices:
    print("No devices found. Exiting script...")
    os.system('pause')
    exit(1)

# Print all connected devices
print("Connected devices:")
for port, description in connected_devices:     
    print(f"- {description}: {port}")

if bypass:
    choice = input("Run tests before flashing? (Y/N): ").strip().lower()
    if choice == "y":
        run_tests()
    elif choice == "n":
        flash_firmware()
    else:
        print("Invalid choice. Please enter Y or N.")
else:
    flash_firmware()

os.system('pause')