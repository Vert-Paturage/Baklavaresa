import Table from './table.type';
import Schedule from './schedule.type';

type Reservation = {
    ID: number;
	Date: Schedule;
	NumberOfPeople: number;
	Tables: Table[];
    FirstName: string;
	LastName: string;
	Email: string;
};

export default Reservation;