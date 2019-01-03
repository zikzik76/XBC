
# Notes!
 - Download Git Bash : [Here](https://git-scm.com/download/win).
 - Register Accout Github : [Here](https://github.com/).

# How to ???

- Buka atau buat folder kosong dimanapun didirektori kalian
- Klik kanan "Git Bash Here"
- Type pada terminal Git Bash-nya
	> git clone https://github.com/ridhoemgl/XBC.git

- Type  agar kalian mendapatkan commit/ update terbaru dari branch development
	> git pull 

	contoh (Pada saat git pull harus di branch development):
	   
	   ![enter image description here](https://lh3.googleusercontent.com/75LN1sKB7iKs6ccDGF2Jyptc0yJMR4ro0y08hccPmcUyYqnWL6bjcTu5seIzSRBgw9_x3XtuJlN3)

- Buat branch baru sesuai nama modul atau tabel yang dikerjakan
	> git branch bootcamp_type
	
	contoh: 
		![enter image description here](https://lh3.googleusercontent.com/sNnnub2sjj5Oc-FYe3FKFqJN3IsTOuMR237PCGikwh8PbZIvfSCGG2wAhIXHUMBXGDDKD6WwTFi1)

- Pindah ke branch yang baru dibuat
   > git checkout bootcamp_type
	
	contoh:
		
		![enter image description here](https://lh3.googleusercontent.com/774sL4M5n59SHsLb0LNhGE5QLG-Rm6CRMLqfL8sREhZFLH1nqsscdXufsHMHIwcscLjGip1H7K9C)

- Type 
	> git rebase development

	untuk memofidifikasi commit yang sudah ada yang tadi kalian pull dari development
	contoh:
		
		![enter image description here](https://lh3.googleusercontent.com/TRrFoN8U9NTfqIw9qtYKMNhN6v_M0L0POnkWeVZ_s4AdKeLgkmNfzRuhJGZKw0MZAYmjXPHGoF7R)

- Add manual 1/1 project pada masing-masing class pada Visual Studio kalian
	- contoh : Controllers, Views, ViewModel, Repo dan sebagainya
	**JANGAN DRAG AND DROP**
- Setelah Selesai menambahkan klik kanan **Solution** pada project yang sebelumnya di clone dan pilih **Manage Nudget Packages for Solution** lalu klik **Restore** (di pojok kanan).
- Setelah selesai, Type
	> git add .

	contoh:
		
		![enter image description here](https://lh3.googleusercontent.com/grbwSJahq6ymHY3iZCQqbiSHCsn515AQOR7mmnYQVQQdMlIehQiphtYeXBtwsaE9yrFKoF1PkI2K)
	
- Type "git commit -m "KALIAN_MENGERJAKAN_FITUR_APA" (dengan petik)
	contoh:
	> git commit -m "bootcamp_type CRUD added"
	
- Type "git push origin [NAMA_BRANCH]"
	>git push origin bootcamp_type
	
	contoh:
		
		![enter image description here](https://lh3.googleusercontent.com/eiGh79BS9RRlZuzIzqL426_xe9orXr7zuTT3UdlsQJ96JwCtx7DmBuu4AXuV4n_hbVGVYEKpzGie)

- Klik **Compare & Pull Request** Pada Github Web
	
	![enter image description here](https://lh3.googleusercontent.com/8JQY0qfIowtwU_qk7PY5R6orH-xs40llQHX8Ue0-hba9-cDP79BuoV6Sg1HbHZz_4AZbQCT0yoYt)

- Klik **Create pull request**
	
	![enter image description here](https://lh3.googleusercontent.com/msyhUPuT2LM0FA3BGPh1rFOAVDDOfiCGRJFiBQVxZoHUbG-1zYS8MnXbWFh50v8kHinlbubxvO6J)

- Klik **Merge pull request** lalu **Confirm Merge**
	
	![enter image description here](https://lh3.googleusercontent.com/OYGRYy9ukS2GDq4RGF_U9aZgzqTrHhP_cCbCGaF41ZG4EWqf77tbps1Y6-VGs7pTmKHzmi-SooAx)

- Setelah itu kembali ke Terminal
	- Pindah branch ke development
		> git checkout development

	 - lalu **Delete Branch** yang tadi dibuat
		 > git branch -D bootcamp_type

		contoh:
			
			![enter image description here](https://lh3.googleusercontent.com/O7xGMhCH7TykrrmHgduMR449h1Q1UusrsoeevF5mAJqABZRPe0KV54OJnc2z-Z2z8p-yPKvSG347)

- Selesai