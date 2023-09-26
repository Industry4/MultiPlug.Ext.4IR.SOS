all:
	chmod +x ./adaptorsos/DEBIAN/preinst
	chmod +x ./adaptorsos/DEBIAN/postinst
	chmod +x ./adaptorsos/DEBIAN/prerm
	chmod +x ./adaptorsos/DEBIAN/postrm	
	dpkg-deb --build ./adaptorsos
	dpkg-name adaptorsos.deb
	dpkg-scanpackages ./ /dev/null | gzip -9c > ./Packages.gz

clean: 
	  $(RM) adaptorsos.deb
	  $(RM) Packages.gz