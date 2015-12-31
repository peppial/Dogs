module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.initConfig({
        uglify: {
            my_target: {
                files: { 'wwwroot/js/app.js': ['Scripts/app.js', 'Scripts/**/*.js'] }
            },
            options: {
                mangle: false
            }
        },

        watch: {
            scripts: {
                files: ['Scripts/**/*.js'],
                tasks: ['uglify']
            }
        }
    });

    grunt.registerTask('default', ['uglify', 'watch']);
};