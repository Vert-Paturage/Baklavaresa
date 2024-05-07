import { Time } from '@angular/common';

type OpeningHours = {
	day: string;
	MorningOpening: Time;
	MorningClosing: Time;
	AfternoonOpening: Time;
	AfternoonClosing: Time;
};

export default OpeningHours;