fx_version 'bodacious'
game 'gta5'

files {  
    'Config.yaml'
}

server_script 'src/**.net.dll'


author 'zabbix-byte'
version '1.0.0'
description 'This is an connector for mysql database'

server_exports {"raw", "select", "insert"}
