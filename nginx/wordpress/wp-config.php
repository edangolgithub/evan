<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the
 * installation. You don't have to use the web site, you can
 * copy this file to "wp-config.php" and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * MySQL settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://wordpress.org/support/article/editing-wp-config-php/
 *
 * @package WordPress
 */

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define( 'DB_NAME', 'wpnginx' );

/** MySQL database username */
define( 'DB_USER', 'root' );

/** MySQL database password */
define( 'DB_PASSWORD', 'mysqlpassword' );

/** MySQL hostname */
define( 'DB_HOST', 'localhost' );

/** Database Charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The Database Collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         'brP:0%/e8)*x*zHJ09;NLXMXZXufS;zoobgC.H_T$M<}/9gwgpamFl/jxqoH9UV/' );
define( 'SECURE_AUTH_KEY',  '|_L&$j:,:}aFsoS.a/=o8()(s3Vop, al)90U=#-r0Ir0q Pw:je%%n;dKvjsEgW' );
define( 'LOGGED_IN_KEY',    ';T2+52MA[Fl@mr[t0 ^U#hoax{O5!KFaZ!x@M>kjZ5eH0bRr}Zs~-_A6OJ%>M:=F' );
define( 'NONCE_KEY',        '=xkiT*`ToRM+U]2b&:Pe;LrqqH/M#!P-iS4P1!(rI%kaY^dbXr|aW$f.~e^!>&<A' );
define( 'AUTH_SALT',        'a  RL|;3eV=Z-YtDLtHeGMFf#g4 !d8Ppfr9oxZkNj??( xQ|;JR{<**oK[49;q*' );
define( 'SECURE_AUTH_SALT', 'W9S&*5vlWK%|NV0}ss*|f[RT9rNBX.g`/Eh`fp)>z=P2rxjCxZ`IIJGHe/;<YW)R' );
define( 'LOGGED_IN_SALT',   'sx~}<C,%7;o1/<()uo#w1yHu?sO|sDs/aTSq})iu((B,bx^+W+BRcLhuf!Q%l#Nd' );
define( 'NONCE_SALT',       '=TUY>E Eh3{0-<h73?{R6@uiiHt7;&DH8L:kL(4(m)1*L#s>pk)oaV<r 4,0^Y}b' );

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 */
$table_prefix = 'wp_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the documentation.
 *
 * @link https://wordpress.org/support/article/debugging-in-wordpress/
 */
define( 'WP_DEBUG', false );

/* That's all, stop editing! Happy publishing. */

/** Absolute path to the WordPress directory. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', __DIR__ . '/' );
}

/** Sets up WordPress vars and included files. */
require_once ABSPATH . 'wp-settings.php';
define('FS_METHOD','direct');