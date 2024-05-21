{
  description = "Web dev Env";
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  };
  outputs = {nixpkgs, ...} @ inputs:
    let
    system = "x86_64-linux";
    pkgs = import nixpkgs {
      system = system;
      config.allowUnfree = true;
    };
    lib = pkgs.lib;
    in {
      devShells.${system}.default = pkgs.mkShell {
      nativeBuildInputs = with pkgs; [
        (with nodePackages;[
         eslint
         typescript
         typescript-language-server
         vscode-css-languageserver-bin
         vscode-html-languageserver-bin
         ])
         nodePackages."@angular/cli"
         # nodePackages."@angular/language-server"
      ];
      shellHook = ''
        # export PATH=$PATH:${pkgs.nodePackages.vscode-html-languageserver-bin}/bin:${pkgs.nodePackages.vscode-css-languageserver-bin}/bin:${pkgs.nodePackages.typescript-language-server}/bin:${pkgs.nodePackages.eslint}/bin:${pkgs.nodePackages.typescript}/bin:${pkgs.nodePackages.vscode-json-languageserver-bin}/bin
      '';
    };
  };
}
