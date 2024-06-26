module.exports = function (config) {
	config.set({
		basePath: '',
		frameworks: ['jasmine', '@angular-devkit/build-angular'],
		plugins: [
			require('karma-jasmine'),
			require('karma-firefox-launcher'),
			require('karma-jasmine-html-reporter'),
			require('karma-coverage'),
			require('@angular-devkit/build-angular/plugins/karma')
		],
		client: {
			jasmine: {
			},
			clearContext: false
		},
		jasmineHtmlReporter: {
			suppressAll: true
		},
		coverageReporter: {
			dir: require('path').join(__dirname, './coverage/baklavaresa-frontend'),
			subdir: '.',
			reporters: [
			{ type: 'html' },
			{ type: 'text-summary' }
			]
		},
		reporters: ['progress', 'kjhtml'],
		browsers: ['Firefox'],
		restartOnFileChange: true
	});
};
