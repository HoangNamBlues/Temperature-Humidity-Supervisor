################################################################################
# Automatically-generated file. Do not edit!
# Toolchain: GNU Tools for STM32 (11.3.rel1)
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../My-Drivers/Src/DHT22.c \
../My-Drivers/Src/LCD_I2C.c \
../My-Drivers/Src/LoRa.c 

OBJS += \
./My-Drivers/Src/DHT22.o \
./My-Drivers/Src/LCD_I2C.o \
./My-Drivers/Src/LoRa.o 

C_DEPS += \
./My-Drivers/Src/DHT22.d \
./My-Drivers/Src/LCD_I2C.d \
./My-Drivers/Src/LoRa.d 


# Each subdirectory must supply rules for building sources it contributes
My-Drivers/Src/%.o My-Drivers/Src/%.su My-Drivers/Src/%.cyclo: ../My-Drivers/Src/%.c My-Drivers/Src/subdir.mk
	arm-none-eabi-gcc "$<" -mcpu=cortex-m3 -std=gnu11 -g3 -DDEBUG -DUSE_HAL_DRIVER -DSTM32F103xB -c -I../Core/Inc -I"D:/My-Projects/STM32/STM32CUBEIDE/STM32F103/Projects_2023/LoRa_Sender_Node/My-Drivers/Inc" -I../Drivers/STM32F1xx_HAL_Driver/Inc -I../Drivers/STM32F1xx_HAL_Driver/Inc/Legacy -I../Drivers/CMSIS/Device/ST/STM32F1xx/Include -I../Drivers/CMSIS/Include -O0 -ffunction-sections -fdata-sections -Wall -fstack-usage -fcyclomatic-complexity -MMD -MP -MF"$(@:%.o=%.d)" -MT"$@" --specs=nano.specs -mfloat-abi=soft -mthumb -o "$@"

clean: clean-My-2d-Drivers-2f-Src

clean-My-2d-Drivers-2f-Src:
	-$(RM) ./My-Drivers/Src/DHT22.cyclo ./My-Drivers/Src/DHT22.d ./My-Drivers/Src/DHT22.o ./My-Drivers/Src/DHT22.su ./My-Drivers/Src/LCD_I2C.cyclo ./My-Drivers/Src/LCD_I2C.d ./My-Drivers/Src/LCD_I2C.o ./My-Drivers/Src/LCD_I2C.su ./My-Drivers/Src/LoRa.cyclo ./My-Drivers/Src/LoRa.d ./My-Drivers/Src/LoRa.o ./My-Drivers/Src/LoRa.su

.PHONY: clean-My-2d-Drivers-2f-Src

