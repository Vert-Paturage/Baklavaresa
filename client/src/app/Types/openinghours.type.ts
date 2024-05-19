import { Time } from '@angular/common';

type OpeningHours = {
	day: string;
	morningOpening: Time;
	morningClosing: Time;
	afternoonOpening: Time;
	afternoonClosing: Time;
};

export default OpeningHours;