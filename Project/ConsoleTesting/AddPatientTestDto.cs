namespace ConsoleTesting;

public record AddPatientTestDto(bool Active, string Gender, DateTime Birthdate, AddPatientDetailsTestDto Name);

public record AddPatientDetailsTestDto(string Use, string Family, List<string> Given);