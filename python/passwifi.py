import subprocess
meta_data = subprocess.check_output(['netsh','wlan','show','profiles'])
data = meta_data.decode('uft-8', errors="backslashreplace")
data = data.split('\n')
profiles = []

for i in data:
    if 'All User Profile' in i:
        i = i.split(':')
        i = i[1]
        i = i[1:-1]
    profiles.append(i)

print({"{:_<30}"})    