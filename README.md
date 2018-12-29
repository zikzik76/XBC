# Notes!

 - Download Git Bash : [Here](https://git-scm.com/download/win).
 - Register Accout Github : [Here](https://github.com/).

# How to ???

- Buka atau buat folder kosong dimanapun didirektori kalian
- Klik kanan "Git Bash Here"
- Type "git clone https://github.com/ridhoemgl/XBC.git" pada terminal Git Bash-nya
- Type "git pull development" agar kalian mendapatkan commit/ update terbaru dari branch development
- Buat branch baru sesuai nama modul atau tabel yang dikerjakan
	- Contoh : "git branch [NAMA_MODUL/TABEL]" || "git branch bootcamp_type"
- Pindah ke branch yang baru dibuat
	- Contoh : "git checkout [NAMA_MODUL/TABEL]" || "git checkout bootcamp_type"
- Type "git rebase development" untuk memofidifikasi commit yang sudah ada yang tadi kalian pull dari development
- Copy atau buat project pada masing-masing class pada Visual Studio kalian
	- Contoh : Controlles, Views, ViewModel, Repo dan sebagainya
- Setelah modul selesai dikerjakan, type "git add ." || dengan titik
- Type "git commit -m "KALIAN_MENGERJAKAN_FITUR_APA" "
	- Contoh : "git commit -m "bootcamp_type CRUD added" "
- Type "git push origin [NAMA_MODUL/TABEL]" || [NAMA_MODUL/TABEL] = nama branch yang tadi kalian buat
	- Contoh "git push origin bootcamp_type"
- Selesai