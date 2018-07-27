# This is a shell script that calls functions and scripts from
# tml@iki.fi's personal work environment. It is not expected to be
# usable unmodified by others, and is included only for reference.

MOD=zlib
VER=1.2.4
REV=2
ARCH=win32

THIS=${MOD}_${VER}-${REV}_${ARCH}

RUNZIP=${THIS}.zip
DEVZIP=${MOD}-dev_${VER}-${REV}_${ARCH}.zip

HEX=`echo $THIS | md5sum | cut -d' ' -f1`
TARGET=c:/devel/target/$HEX

usedev

(

set -x

UPSTREAM_ZIP=/c/tmp/downloads/zlib124-dll.zip
T=/tmp/zlib-$$

mkdir -p $TARGET/{bin,include,lib} &&
mkdir $T &&

cd $T
unzip $UPSTREAM_ZIP &&
cd zlib-${VER} &&

dos2unix zlib.h &&
dos2unix zconf.h &&

patch -p0 <<'EOF'
--- zlib.h
+++ zlib.h
@@ -1556,38 +1556,12 @@
         inflateBackInit_((strm), (windowBits), (window), \
                                             ZLIB_VERSION, sizeof(z_stream))
 
-#ifdef _LARGEFILE64_SOURCE
-   ZEXTERN gzFile ZEXPORT gzopen64 OF((const char *, const char *));
-   ZEXTERN off64_t ZEXPORT gzseek64 OF((gzFile, off64_t, int));
-   ZEXTERN off64_t ZEXPORT gztell64 OF((gzFile));
-   ZEXTERN off64_t ZEXPORT gzoffset64 OF((gzFile));
-   ZEXTERN uLong ZEXPORT adler32_combine64 OF((uLong, uLong, off64_t));
-   ZEXTERN uLong ZEXPORT crc32_combine64 OF((uLong, uLong, off64_t));
-#endif
-
-#if !defined(ZLIB_INTERNAL) && _FILE_OFFSET_BITS == 64
-#  define gzopen gzopen64
-#  define gzseek gzseek64
-#  define gztell gztell64
-#  define gzoffset gzoffset64
-#  define adler32_combine adler32_combine64
-#  define crc32_combine crc32_combine64
-#  ifndef _LARGEFILE64_SOURCE
-     ZEXTERN gzFile ZEXPORT gzopen64 OF((const char *, const char *));
-     ZEXTERN off_t ZEXPORT gzseek64 OF((gzFile, off_t, int));
-     ZEXTERN off_t ZEXPORT gztell64 OF((gzFile));
-     ZEXTERN off_t ZEXPORT gzoffset64 OF((gzFile));
-     ZEXTERN uLong ZEXPORT adler32_combine64 OF((uLong, uLong, off_t));
-     ZEXTERN uLong ZEXPORT crc32_combine64 OF((uLong, uLong, off_t));
-#  endif
-#else
    ZEXTERN gzFile ZEXPORT gzopen OF((const char *, const char *));
    ZEXTERN z_off_t ZEXPORT gzseek OF((gzFile, z_off_t, int));
    ZEXTERN z_off_t ZEXPORT gztell OF((gzFile));
    ZEXTERN z_off_t ZEXPORT gzoffset OF((gzFile));
    ZEXTERN uLong ZEXPORT adler32_combine OF((uLong, uLong, z_off_t));
    ZEXTERN uLong ZEXPORT crc32_combine OF((uLong, uLong, z_off_t));
-#endif
 
 #if !defined(ZUTIL_H) && !defined(NO_DUMMY_DECL)
     struct internal_state {int dummy;}; /* hack for buggy compilers */
--- zconf.h
+++ zconf.h
@@ -375,10 +375,6 @@
 #  endif
 #endif
 
-#ifdef _LARGEFILE64_SOURCE
-#  include <sys/types.h>
-#endif
-
 #ifndef SEEK_SET
 #  define SEEK_SET        0       /* Seek from beginning of file.  */
 #  define SEEK_CUR        1       /* Seek from current position.  */
EOF

pexports zlib1.dll >zlib.def &&
dlltool --input-def zlib.def --dllname zlib1.dll --output-lib libz.dll.a &&
cp zlib1.dll $TARGET/bin &&
cp zdll.lib libz.dll.a zlib.def $TARGET/lib &&
cp zlib.h zconf.h $TARGET/include &&

rm -f /tmp/$RUNZIP /tmp/$DEVZIP &&
(cd /devel/target/$HEX &&
zip /tmp/$RUNZIP bin/zlib1.dll &&
zip -r -D /tmp/$DEVZIP lib include
)

) 2>&1 | tee /devel/src/tml/packaging/$THIS.log

(cd /devel && zip /tmp/$DEVZIP src/tml/packaging/$THIS.{sh,log}) &&
manifestify /tmp/$RUNZIP /tmp/$DEVZIP
